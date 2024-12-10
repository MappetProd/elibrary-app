using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.DTO
{
    public class CartItemDTO
    {
        public string Id { get; set; }
        public MinimizedPrintedBookDTO PrintedBook { get; set; }
    }
}
