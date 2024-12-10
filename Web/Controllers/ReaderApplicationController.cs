using EL.Service.Contract;
using EL.Service.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EL.Web.Controllers
{
    [Authorize(Roles = "reader")]
    [Route("ReaderApplication")]
    public class ReaderApplicationController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IApplicationService _applicationService;
        public ReaderApplicationController([FromServices] ICartService cartService,
            [FromServices] IApplicationService applicationService)
        {
            _cartService = cartService;
            _applicationService = applicationService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            ReaderApplicationsViewModel viewModel = new ReaderApplicationsViewModel(HttpContext);
            Claim? userId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
            if (userId == null) return BadRequest();

            viewModel.SentApplications = _applicationService.GetSentAplications(userId.Value);
            viewModel.ApprovedApplications = _applicationService.GetResolvedApplications(userId.Value);
            viewModel.EndedApplications = _applicationService.GetEndedApplications(userId.Value);
            return View("/Pages/ReaderApplications.cshtml", viewModel);
        }

        [HttpPut("CreateApplication")]
        public IActionResult CreateApplication()
        {
            Claim? userId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
            if (userId == null) return BadRequest();

            bool applicationResult = _applicationService.Create(userId.Value);
            if (!applicationResult) return BadRequest();

            bool cartResult = _cartService.RemoveAll(userId.Value);
            if (!cartResult) return BadRequest();

            return RedirectToAction("Index", "ReaderApplication");
        }
    }
}
