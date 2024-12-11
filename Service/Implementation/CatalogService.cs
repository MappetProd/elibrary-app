using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using EL.Domain;
using EL.Service.DTO;
using EL.Service.Contract;
using EL.Service.InputModel;
using EL.Repository.Contracts;
using EL.Repository.DTO;

namespace EL.Service.Implementation
{
    public class CatalogService : ICatalogService
    {
        private readonly IPrintedBookRepository _printedBookRepository;

        public CatalogService(IPrintedBookRepository printedBookRepository)
        {
            _printedBookRepository = printedBookRepository;
        }

        private List<DTO.AuthorDTO> ConvertAuthorsToDTO(IEnumerable<Author> authors)
        {
            return (from author in authors
                    select new DTO.AuthorDTO()
                    {
                        Name = author.Name,
                        Surname = author.Surname
                    }).ToList();
        }

        public IEnumerable<PrintedBookDTO> GetAllPrintedBooks()
        {
            IEnumerable<PrintedBookDTO> books = from pb in _printedBookRepository.GetAll().ToList()
                                                select new PrintedBookDTO()
                                                 {
                                                     Id = pb.Id.ToString(),
                                                     Title = pb.Book.Name,
                                                     Authors = ConvertAuthorsToDTO(pb.Book.Authors),
                                                     AmountLeft = pb.AmountLeft
                                                 };
            return books;
        }

        public IEnumerable<PrintedBookDTO> GetAllPrintedBooks(PrintedBookSearchInputModel queryInput)
        {
            var result = _printedBookRepository.Filter(queryInput.SelectedGenres, queryInput.SelectedAuthors, queryInput.SelectedPublishers);
            if (!result.Any() && queryInput.SelectedGenres == null && queryInput.SelectedAuthors == null && queryInput.SelectedPublishers == null)
                result = _printedBookRepository.GetAll();

            return from pb in result
                   select new PrintedBookDTO()
                   {
                       Id = pb.Id.ToString(),
                       Title = pb.Book.Name,
                       Authors = ConvertAuthorsToDTO(pb.Book.Authors),
                       AmountLeft = pb.AmountLeft
                   };
        }
    }
}
