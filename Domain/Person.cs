using MassTransit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Domain;

public class Person : BaseEntity
{
    [Key]
    [Column("person_id")]
    public Guid Id { get; set; } = NewId.NextGuid();

    [Column("person_name")]
    public string Name { get; set; } = null!;

    [Column("person_surname")]
    public string Surname { get; set; } = null!;

    [Column("person_patronymic")]
    public string? Patronymic { get; set; }

    //public virtual ICollection<User> Users { get; set; } = new List<User>();
}
