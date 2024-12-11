using Microsoft.AspNetCore.Http;

namespace EL.Service.ViewModel
{
    public class StatisticsViewModel : BaseViewModel
    {
        public StatisticsViewModel(HttpContext context) : base(context) { }

        public int ReadersAmount { get; set; }
        public int LibrariansAmount { get; set; }
        public int CurrentStockAmount { get; set; }
        public int BooksOfReadersAmount { get; set; }
        public int UniquePrintedBooksAmount { get; set; }
        public int ApplicationsAmount { get; set; }
        public int ClosedApplicationsAmount { get; set; }
    }
}
