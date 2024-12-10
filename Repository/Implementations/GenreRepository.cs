using EL.Domain;
using EL.Repository.Contracts;

namespace EL.Repository.Implementations
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(ElibraryContext context) : base(context) { }

        public IEnumerable<Genre> GetAll()
        {
            return entities.AsEnumerable();
        }
        public Genre? GetByName(string name)
        {
            return entities.SingleOrDefault(g => g.Name == name);
        }
        public IEnumerable<Genre> GetGenresByNames(IEnumerable<string> names)
        {
            List<Genre> genres = new List<Genre>();
            foreach (string name in names)
            {
                Genre? genre = GetByName(name);
                if (genre != null)
                    genres.Add(genre);

                // TODO: ELSE when there is no such genre -> error.
            }

            return genres;
        }
    }
}
