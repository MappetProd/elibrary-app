using MassTransit;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Domain
{
    public partial class Author : Person
    {
        [Column("author_id")]
        public Guid Id { get; set; } = NewId.NextGuid();

        public virtual List<Book> Books { get; set; }
    }
}