using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.InputModel
{
    public class AuthorInputModel
    {
        public string Name { get; set; }
        public string Surname {  get; set; }
        public string? Patronymic { get; set; }
    }
}
