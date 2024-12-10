using EL.Domain;
using EL.Service.DTO;
using EL.Service.InputModel;

namespace EL.Service.Contract
{
    public interface IBookKeepingService
    {
        bool AddPrintedBook(PrintedBookInputModel newBook);
        bool EditPrintedBook(EditPrintedBookInputModel editedBook);
        EditBookFormDTO GetPrintedBook(string id);
        void RemovePrintedBook(string id);
        public bool IsPrintedBookExist(string id);
        public bool IsPrintedBookInStock(string id);
    }
}
