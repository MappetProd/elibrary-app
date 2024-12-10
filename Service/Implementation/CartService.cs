using EL.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;
using EL.Domain;
using EL.Service.Contract;
using EL.Repository.Implementations;
using EL.Service.DTO;

namespace EL.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookKeepingService _bookKeepingService;
        private readonly IRepository<PrintedBook> _printedBookRepository;

        //TODO: garbage collector??
        private Dictionary<string, object> _userCartLockObjects = new Dictionary<string, object>();

        public CartService([FromServices] ICartItemRepository cartItemRepository,
            [FromServices] IUserRepository userRepository,
            [FromServices] IRepository<PrintedBook> printedBookRepository,
            [FromServices] IBookKeepingService bookKeepingService)
        {
            _cartItemRepository = cartItemRepository;
            _userRepository = userRepository;
            _bookKeepingService = bookKeepingService;
            _printedBookRepository = printedBookRepository;
        }

        public IEnumerable<CartItemDTO> GetUserCart(string userId)
        {
            IEnumerable<CartItem> cart = _cartItemRepository.GetUserCart(Guid.Parse(userId));
            return from c in cart
                   select new CartItemDTO
                   {
                       Id = c.Id.ToString(),
                       PrintedBook = new MinimizedPrintedBookDTO
                       {
                           Id = c.PrintedBook.Id.ToString(),
                           PublisherName = c.PrintedBook.Publisher.Name,
                           Authors = from a in c.PrintedBook.Book.Authors
                                     select new AuthorDTO
                                     {
                                         Name = a.Name,
                                         Surname = a.Surname
                                     },
                           PublishingYear = c.PrintedBook.PublishingYear,
                           Title = c.PrintedBook.Book.Name
                       }
                   };
        }

        private object GetUserCartLock(string userId)
        {
            if (!_userCartLockObjects.ContainsKey(userId))
            {
                _userCartLockObjects[userId] = new object();
            }

            return _userCartLockObjects[userId];
        }

        public bool AddPrintedBookToUserCart(string userId, string bookId)
        {
            User? user = _userRepository.Get(userId);
            if (user == null) return false;

            CartItem? itemWithSameBook = _cartItemRepository.GetItem(Guid.Parse(userId), Guid.Parse(bookId));
            if (itemWithSameBook != null) return false;

            PrintedBook? pb = _printedBookRepository.Get(Guid.Parse(bookId));
            if (pb == null || pb.AmountLeft <= 0) return false;

            object currUserCartLock = GetUserCartLock(userId);
            lock (currUserCartLock)
            {
                CartItem cartItem = new CartItem
                {
                    PrintedBook = pb,
                    User = user,
                    AdditionDtm = DateTime.UtcNow
                };
                _cartItemRepository.Insert(cartItem);
            }
            return true;
        }

        public bool RemoveAll(string userId)
        {
            User? user = _userRepository.Get(userId);
            if (user == null) return false;

            object currUserCartLock = GetUserCartLock(userId);
            lock (currUserCartLock)
            {
                _cartItemRepository.ClearCart(Guid.Parse(userId));
            }
            return true;
        }

        public bool RemoveItemByCardItemId(string cartItemId)
        {
            CartItem? cartItem = _cartItemRepository.GetItem(Guid.Parse(cartItemId));
            if (cartItem == null) return false;

            object currUserCartLock = GetUserCartLock(cartItem.User.Id.ToString());
            lock (currUserCartLock)
            {
                try
                {
                    _cartItemRepository.Remove(cartItem);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Trying to remove cart item, that doesn't exist");
                    return false;
                }
            }

            return true;
        }

        public bool RemoveItemByPrintedBook(string userId, string printedBookId)
        {
            User? user = _userRepository.Get(userId);
            if (user == null) return false;

            object currUserCartLock = GetUserCartLock(userId);
            lock (currUserCartLock)
            {
                CartItem? cartItem = _cartItemRepository.GetItem(user.Id, Guid.Parse(printedBookId));
                if (cartItem == null) return false;

                _cartItemRepository.Remove(cartItem);
            }
            return true;
        }
    }
}
