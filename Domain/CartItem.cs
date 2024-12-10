using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Domain
{
    public class CartItem : BaseEntity
    {
        [Key]
        [Column("cart_item_id")]
        public Guid Id { get; set; }

        [Column("addition_dtm")]
        [NotNull]
        public DateTime AdditionDtm { get; set; }

        [Column("printed_book_id")]
        [NotNull]
        public Guid PrintedBookId { get; set; }

        [Column("user_id")]
        [NotNull]
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
        public virtual PrintedBook PrintedBook { get; set; }
    }
}
