using EL.Domain;
using EL.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Repository.Implementations
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ElibraryContext context) : base(context) { }

        public IEnumerable<CartItem> GetUserCart(Guid userId)
        {
            return (from c in entities
                   where c.UserId == userId
                   select c).ToList();
        }

        public void ClearCart(Guid userId)
        {
            IEnumerable<CartItem> userCart = (from c in entities
                                                where c.UserId == userId
                                                select c).ToList();
            entities.RemoveRange(userCart);
            context.SaveChanges();
        }

        public CartItem? GetItem(Guid userId, Guid printedBookId)
        {
            CartItem? cartItem = (from c in entities
                                  where c.UserId.Equals(userId) && c.PrintedBookId.Equals(printedBookId) 
                                  select c).SingleOrDefault();
            return cartItem;
        }

        public CartItem? GetItem(Guid cartItemId)
        {
            CartItem? cartItem = (from c in entities
                                  where c.Id.Equals(cartItemId)
                                  select c).SingleOrDefault();
            return cartItem;
        }
    }
}
