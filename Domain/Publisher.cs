using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MassTransit;

namespace EL.Domain
{
    public class Publisher : BaseEntity
    {
        [Key]
        [Column("publisher_id")]
        [NotNull]
        public Guid Id { get; set; } = NewId.NextGuid();

        [Column("publisher_name")]
        [NotNull]
        public string Name { get; set; }
        
        [Column("publisher_city_name")]
        [NotNull]
        public string CityName { get; set; }

        public virtual List<PrintedBook> PrintedBooks { get; set; }
    }
}
