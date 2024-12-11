using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.Contract
{
    public interface IStatisticsService
    {
        int GetReadersCount();

        int GetLibrariansCount();

        int GetUniquePrintedBooksCount();
        int GetCurrentBooksOfReaders();

        int GetApplicationsCount();
        int GetCurrentBooksStockCount();

        int GetClosedApplicationsCount();
    }
}
