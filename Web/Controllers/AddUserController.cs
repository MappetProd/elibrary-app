using EL.Service.Contract;
using EL.Service.InputModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EL.Web.Controllers
{
    [Route("Register")]
	[Authorize(Roles = "librarian")]
	public class AddUserController : Controller
    {
        private const string _HOME_PAGE_REF = "~/Pages/Index.cshtml";
        private const string _REGISTER_PAGE_REF = "~/Pages/AddUser.cshtml";

        private readonly IAccountService _accountService;
        public AddUserController([FromServices] IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View(_REGISTER_PAGE_REF);
        }

        [HttpPost("CreateUser")]
        public IResult CreateUser(UserInputModel userInputData)
        {
            if (!ModelState.IsValid)
            {
                return Results.BadRequest();
            }

            _accountService.AddUser(userInputData);
            return Results.Ok();
        }
    }
}
