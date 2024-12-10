using EL.Domain;
using EL.Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Repository.Contracts
{
    public interface IPrintedBookRepository : IGenericRepository<PrintedBook>
    {
        /*IEnumerable<PrintedBook> GetByGenres(IEnumerable<string> genreNames);
        IEnumerable<PrintedBook> GetByAuthors(IEnumerable<AuthorDTO> authors);
        IEnumerable<PrintedBook> GetByPublishers(IEnumerable<string> publisherNames);*/
        IEnumerable<PrintedBook> Filter(string? genreNames, string? authorsInitials, string? publisherNames);
    }
}
