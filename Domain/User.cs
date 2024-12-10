using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EL.Domain;

public partial class User : Person
{
    [Column("user_login")]
    public string Login { get; set; } = null!;

    [Column("user_password")]
    public string Password { get; set; } = null!;

    [Column("user_phone_number")]
    public string PhoneNumber { get; set; } = null!;

    [Column("user_birth_date")]
    public DateOnly BirthDate { get; set; }

    [Column("user_status_id")]
    public int StatusId { get; set; }

    public virtual Status Status { get; set; }
}
