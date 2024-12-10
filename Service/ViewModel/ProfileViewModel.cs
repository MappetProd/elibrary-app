using EL.Service.DTO;
using Microsoft.AspNetCore.Http;

namespace EL.Service.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileViewModel(HttpContext context) : base(context) { }

        public UserDTO User { get; set; }
    }
}
