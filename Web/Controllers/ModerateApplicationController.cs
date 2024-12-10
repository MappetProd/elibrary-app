using EL.Domain;
using EL.Service.Contract;
using EL.Service.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EL.Web.Controllers
{
    [Route("ModerateApplication")]
    [Authorize(Roles = "librarian")]
    public class ModerateApplicationController : Controller
    {
        //private readonly ICartService _cartService;
        private readonly IApplicationService _applicationService;
        public ModerateApplicationController([FromServices] ICartService cartService,
            [FromServices] IApplicationService applicationService)
        {
            //_cartService = cartService;
            _applicationService = applicationService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            Claim? userId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
            if (userId == null) return BadRequest();

            ModeratingApplicationViewModel viewModel = new ModeratingApplicationViewModel(HttpContext);
            viewModel.ResolveRequiredApplications = _applicationService.GetResolveRequiredApplications();
            viewModel.EndRequiredApplications = _applicationService.GetEndRequiredApplications();
            viewModel.ArchiveApplications = _applicationService.GetArchiveApplications();
            return View("/Pages/ModeratingApplications.cshtml", viewModel);
        }

        [HttpPost("Resolve")]
        public IResult Resolve(string applicationId)
        {
            Claim? userId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
            if (userId == null) return Results.BadRequest();

            bool result = _applicationService.Resolve(applicationId, userId.Value);
            if (!result) return Results.BadRequest();

            return Results.Ok();
        }

        [HttpPost("End")]
        public IResult End(string applicationId)
        {
            Claim? userId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
            if (userId == null) return Results.BadRequest();

            bool result = _applicationService.End(applicationId, userId.Value);
            if (!result) return Results.BadRequest();

            return Results.Ok();
        }
    }
}
