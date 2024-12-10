using EL.Service.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EL.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            BaseViewModel viewModel = new BaseViewModel(HttpContext);
            return View("~/Pages/Index.cshtml", viewModel);
        }
    }
}
