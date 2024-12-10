using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EL.Domain;

public enum UserStatus { ADMIN, READER, LIBRARIAN }

public class Status
{
    [Key]
    [Column("status_id")]
    [NotNull]
    public int Id { get; set; }

    [Column("status_name")]
    [NotNull]
    public string Name { get; set; } = null!;

    //public virtual ICollection<User> Users { get; set; } = new List<User>();
}
