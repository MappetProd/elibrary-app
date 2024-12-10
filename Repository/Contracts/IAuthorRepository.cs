using EL.Domain;
using EL.Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Repository.Contracts
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        IEnumerable<Author>? GetAllBy(string name, string surname, string? patronymic);
        IEnumerable<Author> GetAllByMany(IEnumerable<AuthorDTO> dtos);
    }
}
