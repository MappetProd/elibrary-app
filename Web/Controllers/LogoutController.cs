using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EL.Web.Controllers
{
    [Route("Logout")]
    public class LogoutController : Controller
    {
        [Authorize]
        [HttpGet("ExitProfile")]
        public async Task<IActionResult> ExitProfile()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
