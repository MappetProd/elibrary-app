using EL.Service.Contract;
using EL.Service.InputModel;
using EL.Service.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EL.Web.Controllers
{
    [Route("ChangePassword")]
    [Authorize]
    public class ChangePasswordController : Controller
    {
        private readonly IAccountService _accountService;
        public ChangePasswordController([FromServices] IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            BaseViewModel viewModel = new BaseViewModel(HttpContext);
            return View("/Pages/ChangePassword.cshtml", viewModel);
        }

        [HttpPost("Change")]
        [Authorize]
        public IActionResult Change(ChangePasswordInputModel inputModel)
        {
            BaseViewModel viewModel = new BaseViewModel(HttpContext);
            if (ModelState.IsValid)
            {
                //TODO: secure?
                Claim? userId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
                if (userId == null)
                    return View("/Pages/ChangePassword.cshtml", viewModel);

                if (!_accountService.IsUserPasswordCorrect(userId.Value, inputModel.OldPassword))
                    return View("/Pages/ChangePassword.cshtml", viewModel);

                if (inputModel.NewPassword != inputModel.ConfirmPassword)
                    return View("/Pages/ChangePassword.cshtml", viewModel);

                bool success = _accountService.ChangedPassword(userId.Value, inputModel.NewPassword);
                if (!success)
                    return View("/Pages/ChangePassword.cshtml", viewModel);

                return RedirectToAction("Index");
            }

            return View("/Pages/ChangePassword.cshtml", viewModel);
        }
    }
}
