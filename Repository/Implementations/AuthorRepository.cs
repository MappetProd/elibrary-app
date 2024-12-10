using EL.Domain;
using EL.Repository.Contracts;
using EL.Repository.DTO;
using System.Diagnostics.Eventing.Reader;

namespace EL.Repository.Implementations
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ElibraryContext context) : base(context) { }

        public IEnumerable<Author>? GetAllBy(string name, string surname, string? patronymic)
        {
            if (patronymic != null)
            {
                //TODO: patronymic in db can be null but not in form
                return from a in entities
                       where a.Name == name && a.Surname == surname && a.Patronymic == patronymic
                       select a;
            }
            else
            {
                return from a in entities
                       where a.Name == name && a.Surname == surname
                       select a;
            }
        }

        public IEnumerable<Author> GetAllByMany(IEnumerable<AuthorDTO> dtos)
        {
            List<Author> result = new List<Author>();
            foreach (var entity in dtos)
            {
                IEnumerable<Author>? authors = GetAllBy(entity.Name, entity.Surname, entity.Patronymic);
                if (authors != null)
                    result.AddRange(authors);
            }
            return result;
        }
    }
}
