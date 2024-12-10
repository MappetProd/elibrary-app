using EL.Domain;
using EL.Service.DTO;
using EL.Service.InputModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Service.Contract
{
    public interface ICatalogService
    {
        IEnumerable<PrintedBookDTO> GetAllPrintedBooks();
        IEnumerable<PrintedBookDTO> GetAllPrintedBooks(PrintedBookSearchInputModel query);
    }
}
