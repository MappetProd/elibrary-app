using EL.Service.Contract;
using EL.Service.DTO;
using EL.Service.InputModel;
using EL.Service.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Security.Principal;

namespace EL.Web.Controllers
{
    [Route("Catalog")]
    public class CatalogController : Controller
    {
        private const string BOOKS_OUTPUT_PARTIAL_VIEW_REF = "/Pages/_PartialBooksOutput.cshtml";
        private const string BOOKS_OUTPUT_PARTIAL_LIBRARIAN_VIEW_REF = "/Pages/_PartialBooksOutputLibrarian.cshtml";
        private const string CATALOG_VIEW_REF = "/Pages/Catalog.cshtml";

        private readonly ICatalogService _catalogService;
        private readonly ISuggestedOptionsService _suggestedOptionsService;
        private readonly IBookKeepingService _bookKeepingService;
        private readonly ICartService _cartService;
        public CatalogController(
            [FromServices] ICatalogService catalogService,
            [FromServices] ISuggestedOptionsService suggestedOptionsService,
            [FromServices] IBookKeepingService bookKeepingService,
            [FromServices] ICartService cartService)
        {
            _catalogService = catalogService;
            _suggestedOptionsService = suggestedOptionsService;
            _bookKeepingService = bookKeepingService;
            _cartService = cartService;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            CatalogViewModel viewModel = new CatalogViewModel(HttpContext);
            viewModel.Genres = _suggestedOptionsService.GetAllGenres();
            viewModel.Authors = _suggestedOptionsService.GetAllAuthors();
            viewModel.Publishers = _suggestedOptionsService.GetAllPublishers();
            viewModel.PrintedBooks = _catalogService.GetAllPrintedBooks();

            Claim? claimRole = HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Role);
            if (HttpContext.User.Identity.IsAuthenticated)
                viewModel.UserRole = claimRole.Value;

            return View(CATALOG_VIEW_REF, viewModel);
        }

        [HttpGet("GetBooksWithFilters/{genres?}/{authors?}/{publishers?}")]
        public IActionResult GetBooksWithFilters([FromQuery] string? genres, [FromQuery] string? authors, [FromQuery] string? publishers)
        {
            if (ModelState.IsValid)
            {
                PrintedBookSearchInputModel model = new PrintedBookSearchInputModel
                {
                    SelectedAuthors = authors,
                    SelectedGenres = genres,
                    SelectedPublishers = publishers
                };
                IEnumerable<PrintedBookDTO> filteredPrintedBooks = _catalogService.GetAllPrintedBooks(model);
                if (User.IsInRole("librarian"))
                    return PartialView(BOOKS_OUTPUT_PARTIAL_LIBRARIAN_VIEW_REF, filteredPrintedBooks);

                return PartialView(BOOKS_OUTPUT_PARTIAL_VIEW_REF, filteredPrintedBooks);
            }

            return RedirectToAction("Index", "Catalog");
        }

        [Authorize(Roles = "reader")]
        [HttpPut("AddBookToCart")]
        public IResult AddBookToCart([Required] string userId, [Required] string bookId)
        {
            /*bool isPrintedBookInDB = _bookKeepingService.IsPrintedBookExist(bookId);
            if (!isPrintedBookInDB)
                return Results.NotFound(bookId);

            bool isPrintedBookInStock = _bookKeepingService.IsPrintedBookInStock(bookId);
            if (!isPrintedBookInStock)
                return Results.BadRequest(bookId);

            IIdentity? userIdentity = User.Identity;
            if (userIdentity == null || !userIdentity.IsAuthenticated)
                return Results.BadRequest();

            Claim? userIdClaim = User.Claims.SingleOrDefault(c => c.Type == "user_id");
            if (userIdClaim == null)
                throw new Exception("There is no user_id claim of user");*/

            bool result = _cartService.AddPrintedBookToUserCart(userId, bookId);
            if (result)
                return Results.BadRequest();

            return Results.Ok();
        }
    }
}
