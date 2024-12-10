using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.Contract
{
    public interface ISuggestedOptionsService
    {
        IEnumerable<string> GetAllGenres();
        IEnumerable<DTO.AuthorDTO> GetAllAuthors();
        IEnumerable<string> GetAllPublishers();
    }
}
