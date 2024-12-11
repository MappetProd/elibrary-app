using EL.Domain;
using EL.Repository.Contracts;
using EL.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.Implementation
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<PrintedBook> _printedBookRepository;
        private readonly IApplicationRepository _applicationRepository;

        public StatisticsService([FromServices] IUserRepository userRepository,
            [FromServices] IRepository<PrintedBook> printedBookRepository,
            [FromServices] IApplicationRepository applicationRepository)
        {
            _userRepository = userRepository;
            _printedBookRepository = printedBookRepository;
            _applicationRepository = applicationRepository;
        }

        public int GetReadersCount()
        {
            return _userRepository.GetAllByRole("reader").Count();
        }

        public int GetLibrariansCount()
        {
            return _userRepository.GetAllByRole("librarian").Count();
        }

        public int GetUniquePrintedBooksCount()
        {
            return _printedBookRepository.GetAll().Count();
        }

        public int GetCurrentBooksStockCount()
        {
            int totalPrintedBooksCount = 0;
            foreach(PrintedBook pb in _printedBookRepository.GetAll())
            {
                totalPrintedBooksCount += pb.AmountLeft;
            }
            return totalPrintedBooksCount;
        }

        public int GetCurrentBooksOfReaders()
        {
            int totalPrintedBooksCount = 0;
            foreach (Application a in _applicationRepository.GetAllResolved())
            {
                foreach (PrintedBook pb in a.PrintedBooks)
                {
                    totalPrintedBooksCount += 1;
                }
            }
            return totalPrintedBooksCount;
        }

        public int GetApplicationsCount()
        {
            return _applicationRepository.GetAll().Count();
        }

        public int GetClosedApplicationsCount()
        {
            return _applicationRepository.GetAllArchive().Count();
        }
    }
}
