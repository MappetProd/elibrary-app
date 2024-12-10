using MassTransit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Domain
{
    public class Book : BaseEntity
    {
        [Key] 
        [Column("book_id")]
        public Guid Id { get; set; } = NewId.NextGuid();

        [Column("book_name")]
        public string Name {  get; set; }

        [Column("first_publication_year")]
        public int FirstPublicationYear { get; set; }

        public virtual List<Genre> Genres { get; set; }
        public virtual List<Author> Authors { get; set; }
        public virtual List<PrintedBook> PrintedBooks { get; set; }
    }
}
