using EL.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EL.Service.InputModel;
using EL.Service.Implementation;
using EL.Service.ViewModel;

namespace EL.Web.Controllers
{
    [Authorize(Roles = "librarian")]
    [Route("AddBook")]
    public class AddBookController : Controller
    {
        private const string _ADDBOOK_PAGE_REF = "~/Pages/AddBook.cshtml";

		private readonly IBookKeepingService _bookKeepingService;
        private readonly ISuggestedOptionsService _suggestedOptionsService;
        public AddBookController(
            [FromServices] IBookKeepingService bookKeepingService,
            [FromServices] ISuggestedOptionsService suggestedOptionsService
            )
        {
            _bookKeepingService = bookKeepingService;
            _suggestedOptionsService = suggestedOptionsService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            AddBookViewModel viewModel = new AddBookViewModel(HttpContext);
			viewModel.Genres = _suggestedOptionsService.GetAllGenres();
			viewModel.Publishers = _suggestedOptionsService.GetAllPublishers();
            return View(_ADDBOOK_PAGE_REF, viewModel);
        }

        [HttpPost("CreateBook")]
        public IActionResult CreateBook([FromForm]PrintedBookInputModel printedBookInputModel)
        {
            if (ModelState.IsValid)
            {
                bool result = _bookKeepingService.AddPrintedBook(printedBookInputModel);
                if (result)
                    return RedirectToAction("Index", "Catalog");

                return RedirectToAction("Index", "AddBook");
            }

            return RedirectToAction("Index", "AddBook");
        }
    }
}
