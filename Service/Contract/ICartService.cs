using EL.Service.DTO;

namespace EL.Service.Contract
{
    public interface ICartService
    {
        bool AddPrintedBookToUserCart(string userId, string bookId);
        bool RemoveAll(string userId);

        bool RemoveItemByCardItemId(string cartItemId);
        bool RemoveItemByPrintedBook(string userId, string printedBookId);
        IEnumerable<CartItemDTO> GetUserCart(string userId);
    }
}
