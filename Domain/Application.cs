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
    public class Application : BaseEntity
    {
        [Key]
        [Column("application_id")]
        public Guid Id { get; set; }

        [Column("issued_by_user_id")]
        [NotNull]
        public Guid IssuedByUserId { get; set; }

        [Column("resolved_by_user_id")]
        public Guid? ResolvedByUserId { get; set; }

        [Column("closed_by_user_id")]
        public Guid? ClosedByUserId { get; set; }

        [Column("application_creation_dtm")]
        [NotNull]
        public DateTime CreationDtm { get; set; }
        
        [Column("resolve_dtm")]
        public DateTime? ResolveDtm { get; set; }

        [Column("application_sequence_number")]
        public int ApplicationNumber { get; set; }

        [Column("deadline_date")]
        public DateOnly? DeadlineDate { get; set; }

        [Column("actual_end_dtm")]
        public DateTime? ActualEndDtm { get; set; }


        public virtual List<PrintedBook> PrintedBooks { get; set; }
        public virtual User IssuedBy { get; set; }
        public virtual User? ResolvedBy { get;set; }
        public virtual User? ClosedBy { get; set; }
    }
}
