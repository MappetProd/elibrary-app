using EL.Service.Contract;
using EL.Service.DTO;
using EL.Service.Implementation;
using EL.Service.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;

namespace EL.Web.Controllers
{
    [Authorize(Roles = "reader")]
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IApplicationService _applicationService;
        public CartController([FromServices] ICartService cartService,
            [FromServices] IApplicationService applicationService)
        {
            _applicationService = applicationService;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            Claim? userId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
            if (userId == null) return BadRequest();

            CartViewModel viewModel = new CartViewModel(HttpContext);
            viewModel.cartItems = _cartService.GetUserCart(userId.Value);
            return View("/Pages/Cart.cshtml", viewModel);
        }

        [AllowAnonymous]
        [HttpGet("GetBooksInCart")]
        public IActionResult GetBooksInCart()
        {
            Claim? userId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
            if (userId == null) return NotFound();

            IEnumerable<CartItemDTO> cartItemDTOs = _cartService.GetUserCart(userId.Value);
            List<string> bookInCartIds = (from c in cartItemDTOs
                                              select c.PrintedBook.Id).ToList();

            /*await Response.WriteAsync(JsonSerializer.Serialize(new
            {
                data = bookInCartIds
            }));
            return Results.Ok();*/

            return new JsonResult(bookInCartIds, new JsonSerializerOptions
            {
                ReferenceHandler = null,
                WriteIndented = true
            });
        }

        [HttpPut("AddItem")]
        public IResult AddItem([FromForm] string printedBookId)
        {
            if (ModelState.IsValid)
            {
                Claim? userId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
                if (userId == null) return Results.BadRequest();

                bool result = _cartService.AddPrintedBookToUserCart(userId.Value, printedBookId);
                if (!result) return Results.BadRequest();
                return Results.Ok();
            }
            else
                return Results.BadRequest();
        }

        [HttpDelete("RemoveItemByPrintedBook")]
        public IResult RemoveItemByPrintedBook([FromForm] string printedBookId)
        {
            if (ModelState.IsValid)
            {
                Claim? userId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
                if (userId == null) return Results.BadRequest();
                bool result = _cartService.RemoveItemByPrintedBook(userId.Value, printedBookId);
                if (!result) return Results.BadRequest();
                return Results.Ok();
            }
            else
                return Results.BadRequest();
        }

        [HttpDelete("RemoveItemByCartItemId")]
        public IResult RemoveItemByCartItemId([FromForm] string cartItemId)
        {
            if (ModelState.IsValid)
            {
                Claim? userId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
                if (userId == null) return Results.BadRequest();
                bool result = _cartService.RemoveItemByCardItemId(cartItemId);
                if (!result) return Results.BadRequest();
                return Results.Ok();
            }
            else
                return Results.BadRequest();
        }

    }
}
