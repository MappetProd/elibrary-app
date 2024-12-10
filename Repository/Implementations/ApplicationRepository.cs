using EL.Domain;
using EL.Repository.Contracts;

namespace EL.Repository.Implementations
{
    public class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(ElibraryContext context) : base(context) { }

        public Application? Get(Guid applicationId)
        {
            return entities.SingleOrDefault(a => a.Id.Equals(applicationId));
        }

        public IEnumerable<Application> GetAllByIssuedUser(Guid readerId)
        {
            return (from a in entities
                   where a.IssuedByUserId == readerId
                   select a).ToList();
        }

        public IEnumerable<Application> GetAllSentApplicationsOfReader(Guid readerId)
        {
            return (from a in entities
                    where a.IssuedByUserId == readerId && a.ResolveDtm == null
                    select a).ToList();
        }

        public IEnumerable<Application> GetAllResolvedApplicationsOfReader(Guid readerId)
        {
            return (from a in entities
                    where a.IssuedByUserId == readerId && a.ResolveDtm != null && a.ActualEndDtm == null
                    select a).ToList();
        }

        public IEnumerable<Application> GetAllEndedApplicationsOfReader(Guid readerId)
        {
            return (from a in entities
                    where a.IssuedByUserId == readerId && a.ActualEndDtm != null
                    select a).ToList();
        }

        public IEnumerable<Application> GetAllResolved()
        {
            return (from a in entities
                    where a.ResolveDtm != null && a.DeadlineDate != null && a.ResolvedBy != null && a.ActualEndDtm == null
                    select a).ToList();
        }
        
        public IEnumerable<Application> GetAllWaitingForResolve()
        {
            return (from a in entities
                    where a.ResolveDtm == null
                    select a).ToList();
        }

        public IEnumerable<Application> GetAllWithTodaysDeadline()
        {
            return (from a in entities
                    where a.DeadlineDate == DateOnly.FromDateTime(DateTime.UtcNow)
                    select a).ToList();
        }

        public IEnumerable<Application> GetAllWithOverdueDeadlines()
        {
            return (from a in entities
                    where a.DeadlineDate < DateOnly.FromDateTime(DateTime.UtcNow)
                    select a).ToList();
        }

        public IEnumerable<Application> GetAllBeforeDeadline()
        {
            return (from a in entities
                    where a.DeadlineDate > DateOnly.FromDateTime(DateTime.UtcNow)
                    select a).ToList();
        }

        public IEnumerable<Application> GetAllArchive()
        {
            return (from a in entities
                    where a.ActualEndDtm != null
                    select a).ToList();
        }
    }
}
