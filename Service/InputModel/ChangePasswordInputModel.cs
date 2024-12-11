using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.InputModel
{
    public class ChangePasswordInputModel
    {
        [FromForm(Name = "old_password")]
        public string OldPassword { get; set; }

        [FromForm(Name = "new_password")]
        public string NewPassword { get; set; }

        [FromForm(Name = "new_password_confirm")]
        public string ConfirmPassword { get; set; }
    }
}
