using EL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Repository.Contracts
{
    public interface ICartItemRepository : IGenericRepository<CartItem>
    {
        IEnumerable<CartItem> GetUserCart(Guid userId);
        void ClearCart(Guid userId);
        public CartItem? GetItem(Guid userId, Guid printedBookId);
        public CartItem? GetItem(Guid cartItemId);
    }
}
