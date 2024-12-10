using MassTransit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EL.Domain
{
    public class PrintedBook : BaseEntity
    {
        [Key]
        [Column("printed_book_id")]
        public Guid Id { get; set; } = NewId.NextGuid();

        [Column("isbn")]
        [NotNull]
        public string ISBN { get; set; }
        
        [Column("publisher_id")]
        [NotNull]
        public Guid PublisherId { get; set; }

        /*private Publisher _publisher;*/

        /*[ForeignKey("PublisherId")]*/
        public virtual Publisher Publisher { get; set; }
        /*{
            get
            {
                return _publisher;
            }
            set
            {
                _publisher = value;
                PublisherId = value.Id;
            }
        }*/

        [Column("book_id")]
        [NotNull]
        public Guid BookId { get; set; }

        //private Book _book;

        /*[ForeignKey(nameof(BookId))]*/
        public virtual Book Book { get; set; }
        /*{
            get
            {
                return _book;
            }
            set
            {
                _book = value;
                BookId = value.Id;
            }
        }*/

        [Column("publishing_year")]
        [NotNull]
        public int PublishingYear { get; set; }

        [Column("amount_left")]
        [NotNull]
        public int AmountLeft { get; set; }

        public virtual List<Application> Applications { get; set; }

    }
}
