using EL.Domain;

namespace EL.Repository.Contracts
{
    public interface IApplicationRepository : IGenericRepository<Application>
    {
        public Application? Get(Guid applicationId);


        public IEnumerable<Application> GetAllByIssuedUser(Guid userId);


        public IEnumerable<Application> GetAllWaitingForResolve();
        public IEnumerable<Application> GetAllWithTodaysDeadline();
        public IEnumerable<Application> GetAllWithOverdueDeadlines();
        public IEnumerable<Application> GetAllBeforeDeadline();
        public IEnumerable<Application> GetAllResolved();
        public IEnumerable<Application> GetAllArchive();


        public IEnumerable<Application> GetAllSentApplicationsOfReader(Guid readerId);
        public IEnumerable<Application> GetAllResolvedApplicationsOfReader(Guid readerId);
        public IEnumerable<Application> GetAllEndedApplicationsOfReader(Guid readerId);
        
    }
}
