using EL.Service.DTO;
using Microsoft.AspNetCore.Http;

namespace EL.Service.ViewModel
{
    public class ModeratingApplicationViewModel : BaseViewModel
    {
        public ModeratingApplicationViewModel(HttpContext context) : base(context) { }

        public IEnumerable<ModeratingApplicationDTO> ResolveRequiredApplications { get; set; }
        public IEnumerable<ModeratingApplicationDTO> EndRequiredApplications { get; set; }
        public IEnumerable<ModeratingApplicationDTO> ArchiveApplications { get; set; }

    }
}
