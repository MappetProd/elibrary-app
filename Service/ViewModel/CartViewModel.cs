using EL.Service.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.ViewModel
{
    public class CartViewModel : BaseViewModel
    {
        public CartViewModel(HttpContext context) : base(context) { }

        public IEnumerable<CartItemDTO> cartItems { get; set; }
    }
}
