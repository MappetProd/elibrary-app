using EL.Service.Contract;
using EL.Service.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EL.Web.Controllers
{
    [Route("Statistics")]
    [Authorize(Roles = "owner")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;
        public StatisticsController([FromServices] IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            StatisticsViewModel viewModel = new StatisticsViewModel(HttpContext);
            viewModel.ReadersAmount = _statisticsService.GetReadersCount();
            viewModel.LibrariansAmount = _statisticsService.GetLibrariansCount();
            viewModel.CurrentStockAmount = _statisticsService.GetCurrentBooksStockCount();
            viewModel.BooksOfReadersAmount = _statisticsService.GetCurrentBooksOfReaders();
            viewModel.UniquePrintedBooksAmount = _statisticsService.GetUniquePrintedBooksCount();
            viewModel.ApplicationsAmount = _statisticsService.GetApplicationsCount();
            viewModel.ClosedApplicationsAmount = _statisticsService.GetClosedApplicationsCount();
            return View("/Pages/Statistics.cshtml", viewModel);
        }
    }
}
