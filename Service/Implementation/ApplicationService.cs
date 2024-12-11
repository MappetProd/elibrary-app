using EL.Domain;
using EL.Repository.Contracts;
using EL.Service.Contract;
using EL.Service.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EL.Service.Implementation
{
    public class ApplicationService: IApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<PrintedBook> _printedBookRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICartItemRepository _cartItemRepository;

        private readonly object _createApplicationLock = new object();
        private readonly object _resolveApplicationLock = new object();
        private readonly object _endApplicationLock = new object();

        public ApplicationService([FromServices] IUserRepository userRepository,
            [FromServices] IRepository<PrintedBook> printedBookRepository,
            [FromServices] IApplicationRepository applicationRepository,
            [FromServices] ICartItemRepository cartItemRepository) 
        {
            _userRepository = userRepository;
            _printedBookRepository = printedBookRepository;
            _applicationRepository = applicationRepository;
            _cartItemRepository = cartItemRepository;
        }

        private IEnumerable<MinimizedPrintedBookDTO> GetMinimizedPrintedBooks(IEnumerable<PrintedBook> printedBooks)
        {
            return from pb in printedBooks
                   select new MinimizedPrintedBookDTO
                   {
                       Id = pb.Id.ToString(),
                       PublisherName = pb.Publisher.Name,
                       PublishingYear = pb.PublishingYear,
                       Authors = from a in pb.Book.Authors
                                 select new AuthorDTO
                                 {
                                     Name = a.Name,
                                     Surname = a.Surname
                                 },
                       Title = pb.Book.Name
                   };
        }

        public IEnumerable<ModeratingApplicationDTO> GetResolveRequiredApplications()
        {
            IEnumerable<Application> applicationseToResolve = _applicationRepository.GetAllWaitingForResolve();
            return from a in applicationseToResolve
                   select new ModeratingApplicationDTO
                   {
                       Id = a.Id,
                       UserLogin = a.IssuedBy.Login,
                       ApplicationNumber = a.ApplicationNumber,
                       CreationDtm = a.CreationDtm,
                       MinimizedPrintedBookDTOs = GetMinimizedPrintedBooks(a.PrintedBooks)
                   };
        }

        public IEnumerable<ModeratingApplicationDTO> GetEndRequiredApplications()
        {
            IEnumerable<Application> resolvedApplications = _applicationRepository.GetAllResolved();
            return from a in resolvedApplications
                   select new ModeratingApplicationDTO
                   {
                       Id = a.Id,
                       UserLogin = a.IssuedBy.Login,
                       ResolvedModeratorLogin = a.ResolvedBy!.Login,
                       ApplicationNumber = a.ApplicationNumber,
                       CreationDtm = a.CreationDtm,
                       DeadlineDate = a.DeadlineDate,
                       MinimizedPrintedBookDTOs = GetMinimizedPrintedBooks(a.PrintedBooks),
                       ResolveDtm = a.ResolveDtm,
                   };
        }

        public IEnumerable<ModeratingApplicationDTO> GetArchiveApplications()
        {
            IEnumerable<Application> archiveApplications = _applicationRepository.GetAllArchive();
            return from a in archiveApplications
                   select new ModeratingApplicationDTO
                   {
                       Id = a.Id,
                       UserLogin = a.IssuedBy.Login,
                       ResolvedModeratorLogin = a.ResolvedBy!.Login,
                       ApplicationNumber = a.ApplicationNumber,
                       CreationDtm = a.CreationDtm,
                       DeadlineDate = a.DeadlineDate,
                       MinimizedPrintedBookDTOs = GetMinimizedPrintedBooks(a.PrintedBooks),
                       ResolveDtm = a.ResolveDtm,
                       ActualReturnDate = a.ActualEndDtm,
                       ClosedModeratorLogin = a.ClosedBy!.Login
                   };
        }


        public IEnumerable<ReaderApplicationDTO> GetSentAplications(string userId)
        {
            //TODO: user check?
            IEnumerable<Application> applications = _applicationRepository.GetAllSentApplicationsOfReader(Guid.Parse(userId));
            return from a in applications
                   select new ReaderApplicationDTO
                   {
                       Id = a.Id,
                       ApplicationNumber = a.ApplicationNumber,
                       CreationDtm = a.CreationDtm,
                       MinimizedPrintedBookDTOs = GetMinimizedPrintedBooks(a.PrintedBooks)
                   };
        }

        public IEnumerable<ReaderApplicationDTO> GetResolvedApplications(string userId)
        {
            //TODO: user check?
            IEnumerable<Application> applications = _applicationRepository.GetAllResolvedApplicationsOfReader(Guid.Parse(userId));
            return from a in applications
                   select new ReaderApplicationDTO
                   {
                       Id = a.Id,
                       ApplicationNumber = a.ApplicationNumber,
                       CreationDtm = a.CreationDtm,
                       MinimizedPrintedBookDTOs = GetMinimizedPrintedBooks(a.PrintedBooks),
                       DeadlineDate = (DateOnly)a.DeadlineDate!,
                       ResolveDtm = (DateTime)a.ResolveDtm!
                   };
        }

        public IEnumerable<ReaderApplicationDTO> GetEndedApplications(string userId)
        {
            //TODO: user check?
            IEnumerable<Application> applications = _applicationRepository.GetAllEndedApplicationsOfReader(Guid.Parse(userId));
            return from a in applications
                   select new ReaderApplicationDTO
                   {
                       Id = a.Id,
                       ApplicationNumber = a.ApplicationNumber,
                       CreationDtm = a.CreationDtm,
                       MinimizedPrintedBookDTOs = GetMinimizedPrintedBooks(a.PrintedBooks),
                       DeadlineDate = (DateOnly)a.DeadlineDate!,
                       ResolveDtm = (DateTime)a.ResolveDtm!,
                       ActualReturnDate = (DateTime)a.ActualEndDtm!,
                   };
        }

        public bool Create(string userId)
        {
            User? user = _userRepository.Get(userId);
            if (user == null)
                return false;

            IEnumerable<CartItem> cartItems = _cartItemRepository.GetUserCart(user.Id);
            if (!cartItems.Any())
                return false;

            List<PrintedBook> printedBooks = new List<PrintedBook>();
            foreach (var item in cartItems)
            {
                printedBooks.Add(item.PrintedBook);
            }

            lock (_createApplicationLock)
            {
                foreach (PrintedBook pb in printedBooks)
                {
                    if (pb.AmountLeft - 1 < 0)
                        return false;

                    pb.AmountLeft -= 1;
                }

                Application? lastUserApplication = _applicationRepository.GetLastApplicationOfUser(user.Id);

                Application application = new Application
                {
                    IssuedBy = user,
                    IssuedByUserId = user.Id,
                    CreationDtm = DateTime.UtcNow,
                    PrintedBooks = printedBooks,
                    ApplicationNumber = lastUserApplication == null ? 0 : lastUserApplication.ApplicationNumber + 1,
                };

                _applicationRepository.Insert(application);
                return true;
            }
        }

        public bool Resolve(string applicationId, string resolvedByUserId)
        {
            lock (_resolveApplicationLock)
            {
                Application? application = _applicationRepository.Get(Guid.Parse(applicationId));
                if (application == null) return false;

                User? resolvedBy = _userRepository.Get(resolvedByUserId);
                if (resolvedBy == null) return false;

                application.ResolvedByUserId = resolvedBy.Id;
                application.ResolvedBy = resolvedBy;
                application.ResolveDtm = DateTime.UtcNow;

                DateOnly dateNow = DateOnly.FromDateTime(DateTime.UtcNow);
                //TODO: change for test
                application.DeadlineDate = dateNow.AddMonths(1);
                _applicationRepository.Update(application);
                return true;
            }
        }

        public bool End(string applicationId, string endedByUserId)
        {
            lock (_endApplicationLock)
            {
                Application? application = _applicationRepository.Get(Guid.Parse(applicationId));
                if (application == null) return false;

                User? endedBy = _userRepository.Get(endedByUserId);
                if (endedBy == null) return false;

                application.ActualEndDtm = DateTime.UtcNow;
                application.ClosedBy = endedBy;
                application.ClosedByUserId = endedBy.Id;
                if (DateOnly.FromDateTime((DateTime)application.ActualEndDtm) < application.DeadlineDate)
                {
                    // TODO: do smth...
                }

                foreach (PrintedBook printedBook in application.PrintedBooks)
                {
                    printedBook.AmountLeft += 1;
                }

                _applicationRepository.Update(application);
                return true;
            }
        }
    }
}
