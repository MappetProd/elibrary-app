using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EL.Service.InputModel
{
    public class UserInputModel
    {
        [FromForm(Name="name")]
        public string Name { get; set; }

        [FromForm(Name = "surname")]
        public string Surname { get; set; }

        [FromForm(Name = "patronymic")]
        public string? Patronymic { get; set; }

        [FromForm(Name = "birth_date")]
        public DateOnly BirthDate { get; set; }

        [FromForm(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        [FromForm(Name = "login")]
        public string Login { get; set; }

        [FromForm(Name = "password")]
        public string Password { get; set; }

    }
}
