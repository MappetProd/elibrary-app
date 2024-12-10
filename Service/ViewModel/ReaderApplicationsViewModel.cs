using EL.Service.DTO;
using Microsoft.AspNetCore.Http;

namespace EL.Service.ViewModel
{
    public class ReaderApplicationsViewModel : BaseViewModel
    {
        public ReaderApplicationsViewModel(HttpContext context) : base(context) { }

        public IEnumerable<ReaderApplicationDTO> SentApplications { get; set; }
        public IEnumerable<ReaderApplicationDTO> ApprovedApplications { get; set; }
        public IEnumerable<ReaderApplicationDTO> EndedApplications { get; set; }
    }
}
