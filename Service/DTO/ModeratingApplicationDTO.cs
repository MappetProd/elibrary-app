using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.DTO
{
    public class ModeratingApplicationDTO : ReaderApplicationDTO
    {
        public string UserLogin { get; set; }
        public string? ResolvedModeratorLogin { get; set; }
        public string? ClosedModeratorLogin { get; set; }
    }
}
