using EL.Service.Contract;
using EL.Service.DTO;
using EL.Service.Implementation;
using EL.Service.InputModel;
using EL.Service.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EL.Web.Controllers
{
    [Route("EditBook")]
    [Authorize(Roles = "librarian")]
    public class EditBookController : Controller
    {
        private readonly IBookKeepingService _bookKeepingService;
        private readonly ISuggestedOptionsService _suggestedOptionsService;
        public EditBookController(
            [FromServices] IBookKeepingService bookKeepingService,
            [FromServices] ISuggestedOptionsService suggestedOptionsService)
        {
            _bookKeepingService = bookKeepingService;
            _suggestedOptionsService = suggestedOptionsService;
        }

        [HttpGet("Index")]
        public IActionResult Index([FromQuery] string bookId)
        {
            EditBookFormDTO dto = _bookKeepingService.GetPrintedBook(bookId);
            EditBookViewModel viewModel = new EditBookViewModel(HttpContext);
            viewModel.book = dto;
            viewModel.AllPublishers = _suggestedOptionsService.GetAllPublishers();
            viewModel.AllGenres = _suggestedOptionsService.GetAllGenres();

            return View("/Pages/EditBook.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult EditBook([FromForm] EditPrintedBookInputModel printedBookInputModel)
        {
            if (ModelState.IsValid)
            {
                bool result = _bookKeepingService.EditPrintedBook(printedBookInputModel);
                if (result)
                    return RedirectToAction("Index", "Catalog");

                return RedirectToAction("Index", "AddBook");
            }

            return RedirectToAction("Index", "AddBook");
        }

        [HttpDelete("RemoveBook")]
        public IActionResult RemoveBook(string bookId)
        {
            _bookKeepingService.RemovePrintedBook(bookId);
            return RedirectToAction("Index", "Catalog");
        }
    }
}
