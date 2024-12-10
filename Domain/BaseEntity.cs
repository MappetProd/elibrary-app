using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Domain
{
    public class BaseEntity
    {
        [NotMapped]
        public Guid Id { get; set; }
    }
}
