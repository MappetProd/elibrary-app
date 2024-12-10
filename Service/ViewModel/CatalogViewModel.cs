using EL.Service.DTO;
using Microsoft.AspNetCore.Http;

namespace EL.Service.ViewModel
{
    public class CatalogViewModel : BaseViewModel
    {
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Publishers { get; set; }
        public IEnumerable<AuthorDTO> Authors{ get; set; }
        public IEnumerable<PrintedBookDTO> PrintedBooks { get; set; }
        public string UserRole { get; set; }

        public CatalogViewModel(HttpContext context) : base(context) { }
    }
}
