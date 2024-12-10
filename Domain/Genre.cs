using MassTransit;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EL.Domain
{
    public class Genre
    {
        [Key]
        [Column("genre_id")]
        [NotNull]
        public Guid Id { get; set; } = NewId.NextGuid();

        [Column("genre_name")]
        [NotNull]
        public string Name { get; set; }

        public virtual List<Book> Books { get; set; }
    }
}
