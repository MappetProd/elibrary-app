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

        public IEnumerable<PrintedBookDTO> GetAllPrintedBooks()
        {
            IEnumerable<PrintedBookDTO> books = from pb in _printedBookRepository.GetAll().ToList()
                                                select new PrintedBookDTO()
                                                 {
                                                     Id = pb.Id.ToString(),
                                                     Title = pb.Book.Name,
                                                     Authors = (from author in pb.Book.Authors
                                                                select new DTO.AuthorDTO()
                                                                {
                                                                    Name = author.Name,
                                                                    Surname = author.Surname
                                                                }).ToList()
                                                 };
            return books;
        }

        public IEnumerable<PrintedBookDTO> GetAllPrintedBooks(PrintedBookSearchInputModel queryInput)
        {
            /*List<Repository.DTO.AuthorDTO> authors = new List<Repository.DTO.AuthorDTO>();
            if (queryInput.SelectedAuthors != null)
            {
                foreach (string authorInitials in queryInput.SelectedAuthors.Split(','))
                {
                    string[] author = authorInitials.Split(' ');
                    string authorSurname = author[0];
                    string authorName = author[1];
                    authors.Add(new Repository.DTO.AuthorDTO
                    {
                        Name = authorName,
                        Surname = authorSurname
                    });
                }
            }*/

            var result = _printedBookRepository.Filter(queryInput.SelectedGenres, queryInput.SelectedAuthors, queryInput.SelectedPublishers);
            if (!result.Any() && queryInput.SelectedGenres == null && queryInput.SelectedAuthors == null && queryInput.SelectedPublishers == null)
                result = _printedBookRepository.GetAll();

            return from pb in result
                   select new PrintedBookDTO()
                   {
                       Id = pb.Id.ToString(),
                       Title = pb.Book.Name,
                       Authors = (from author in pb.Book.Authors
                                  select new Service.DTO.AuthorDTO()
                                  {
                                      Name = author.Name,
                                      Surname = author.Surname
                                  }).ToList()
                   };

            /*IEnumerable<PrintedBook> filteredBooks = _printedBookRepository.GetAll().ToList();
            
            if (queryInput.SelectedPublishers != null)
            {
                List<PrintedBook> tempResult = new List<PrintedBook>();
                foreach (string publisherName in queryInput.SelectedPublishers.Split(','))
                {
                    tempResult.AddRange(filteredBooks.Where(pb => pb.Publisher.Name == publisherName));
                }
                filteredBooks = tempResult;
            }

            if (queryInput.SelectedGenres != null)
            {
                List<PrintedBook> tempResult = new List<PrintedBook>();
                foreach (string genreName in queryInput.SelectedGenres.Split(','))
                {
                    tempResult.AddRange(filteredBooks.Where(pb => pb.Book.Genres.Where(g => g.Name == genreName).Any()));
                }
                filteredBooks = tempResult;
            }

            if (queryInput.SelectedAuthors != null)
            {
                List<PrintedBook> tempResult = new List<PrintedBook>();
                foreach (string authorInitials in queryInput.SelectedAuthors.Split(','))
                {
                    string[] author = authorInitials.Split(' ');
                    string authorSurname = author[0];
                    string authorName = author[1];
                    tempResult.AddRange(filteredBooks.Where(pb => pb.Book.Authors.Where(a => a.Name == authorName && a.Surname == authorSurname).Any()));
                }
                filteredBooks = tempResult;
            }

            IEnumerable<PrintedBookDTO> result = from pb in filteredBooks
                                                 select new PrintedBookDTO()
                                                 {
                                                     Id = pb.Id.ToString(),
                                                     Title = pb.Book.Name,
                                                     Authors = (from author in pb.Book.Authors
                                                                select new AuthorDTO()
                                                                {
                                                                    Name = author.Name,
                                                                    Surname = author.Surname
                                                                }).ToList()
                                                 };

            return result;*/
        }
    }
}
