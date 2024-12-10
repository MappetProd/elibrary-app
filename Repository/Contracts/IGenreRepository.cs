using EL.Domain;

namespace EL.Repository.Contracts
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Genre? GetByName(string name);
        IEnumerable<Genre> GetGenresByNames(IEnumerable<string> names);
    }
}
