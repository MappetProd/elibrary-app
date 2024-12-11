using Microsoft.AspNetCore.Mvc;
using EL.Domain;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using EL.Service;
using EL.Service.Contract;
using EL.Service.DTO;
using EL.Service.Implementation;
using EL.Service.ViewModel;
using EL.Service.InputModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EL.Web.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        private const string _HOME_PAGE_REF = "~/Pages/Index.cshtml";
        private const string _PROFILE_PAGE_REF = "~/Pages/Profile.cshtml";

        private readonly IAccountService _accountService;
        //private readonly Dictionary<string, string> _rolePage;
        public LoginController([FromServices] IAccountService accountService)
        {
            _accountService = accountService;
            /*_rolePage = new Dictionary<string, string>
            {
                {"owner", "/Pages/OwnerProfile.cshtml"},
                {"librarian", "/Pages/LibrarianProfile.cshtml"},
                {"reader", "/Pages/ReaderProfile.cshtml"},
            };*/
		}

        [HttpGet("Index")]
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ProfileViewModel profileViewModel = new ProfileViewModel(HttpContext);
                profileViewModel.User = _accountService.ClaimsToDTO(HttpContext.User.Claims);
                return View(_PROFILE_PAGE_REF, profileViewModel);
            }

            BaseViewModel viewModel = new BaseViewModel(HttpContext);
            return View("~/Pages/Login.cshtml", viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(string login, string password)
        {
            ClaimsPrincipal? claimsPrincipal = _accountService.TryLogin(login, password);
            if (claimsPrincipal == null)
                return RedirectToAction("Index", "Home");

            await HttpContext.SignInAsync(claimsPrincipal);

            return RedirectToAction("Index", "Login");
        }

        
    }
}
