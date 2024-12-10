using EL.Domain;
using EL.Repository.Contracts;
using EL.Service.Contract;
using EL.Service.DTO;
using EL.Service.InputModel;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EL.Service.Implementation
{
    public class BookKeepingService : IBookKeepingService
    {
        private readonly IRepository<PrintedBook> _printedBookRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IPublisherRepository _publisherRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BookKeepingService(
            [FromServices] IRepository<PrintedBook> printedBookRepository,
            [FromServices] IGenreRepository genreRepository,
            [FromServices] IPublisherRepository publisherRepository,
            [FromServices] IAuthorRepository authorRepository,
            [FromServices] IBookRepository bookRepository,
            [FromServices] IWebHostEnvironment hostEnvironment)
        {
            _printedBookRepository = printedBookRepository;
            _genreRepository = genreRepository;
            _publisherRepository = publisherRepository;
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _hostEnvironment = hostEnvironment;
        }

        private IEnumerable<EL.Repository.DTO.AuthorDTO> ConvertAuthorsInDTO(IEnumerable<AuthorInputModel> inputAuthors)
        {
            IEnumerable<EL.Repository.DTO.AuthorDTO> convertedAuthors = from a in inputAuthors
                                                      select new EL.Repository.DTO.AuthorDTO
                                                      {
                                                          Name = a.Name,
                                                          Surname = a.Surname,
                                                          Patronymic = a.Patronymic
                                                      };
            return convertedAuthors;
        }

        private Book CreateNewBook(string title, string genres, IEnumerable<Author> authors)
        {
            Book book = new Book
            {
                Name = title,
                Genres = _genreRepository.GetGenresByNames(genres.Split(',')).ToList(),
                Authors = authors.ToList()
            };
            _bookRepository.Insert(book);
            return book;
        }
        
        private Book? TryGetBookFromRepository(IEnumerable<AuthorInputModel> authorsDTOs, string title)
        {
            IEnumerable<EL.Repository.DTO.AuthorDTO> convertedAuthors = ConvertAuthorsInDTO(authorsDTOs);
            IEnumerable<Author> authors = _authorRepository.GetAllByMany(convertedAuthors);
            if (!authors.Any())
                return null;

            Book? book = _bookRepository.GetBook(title, authors);
            return book;
        }

        private Book? GetBookByInput(PrintedBookInputModel inputBook)
        {
            IEnumerable<EL.Repository.DTO.AuthorDTO> convertedAuthors = ConvertAuthorsInDTO(inputBook.Authors);
            IEnumerable<Author> authors = _authorRepository.GetAllByMany(convertedAuthors);
            if (!authors.Any())
                return null;

            Book? book = _bookRepository.GetBook(inputBook.Title, authors);
            if (book == null)
            {
                book = CreateNewBook(inputBook.Title, inputBook.Genres, authors);
            }

            return book;
        }

        public bool AddPrintedBook(PrintedBookInputModel inputBook)
        {
            Publisher? publisher = _publisherRepository.Get(inputBook.PublisherName);
            if (publisher == null)
                return false;

            IEnumerable<EL.Repository.DTO.AuthorDTO> convertedAuthors = ConvertAuthorsInDTO(inputBook.Authors);
            IEnumerable<Author> authors = _authorRepository.GetAllByMany(convertedAuthors);

            Book? book = GetBookByInput(inputBook);
            if (book == null)
                return false;

            PrintedBook newPrintedBook = new PrintedBook
            {
                Id = NewId.NextGuid(),
                Book = book,
                BookId = book.Id,
                Publisher = publisher,
                PublisherId = publisher.Id,
                PublishingYear = inputBook.PublishingYear,
                AmountLeft = inputBook.Amount,
                ISBN = inputBook.ISBN
            };

            if (inputBook.Image != null)
                UploadImageToServer(newPrintedBook.Id.ToString(), inputBook.Image);

            _printedBookRepository.Insert(newPrintedBook);
            
            return true;
        }

        private void UploadImageToServer(string name, IFormFile image)
        {
            string uploads = Path.Combine(_hostEnvironment.WebRootPath, "img/printed_books");
            if (image.Length > 0)
            {
                string filepath = Path.Combine(uploads, $"{name}.jpg");
                using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
        }

        public bool EditPrintedBook(EditPrintedBookInputModel inputBook)
        {
            PrintedBook pb = _printedBookRepository.Get(Guid.Parse(inputBook.Id));

            Publisher? publisher = _publisherRepository.Get(inputBook.PublisherName);
            if (publisher == null)
                return false;

            /*Book? book = GetBookByInput(inputBook);
            if (book == null)
                return false;*/

            pb.ISBN = inputBook.ISBN;
            // TODO: check amount below zero
            pb.AmountLeft = inputBook.Amount;
            pb.PublishingYear = inputBook.PublishingYear;
            pb.Publisher = publisher;

            _printedBookRepository.Update(pb);
            if (inputBook.Image != null)
                UploadImageToServer(pb.Id.ToString(), inputBook.Image);

            return true;
        }
        
        public EditBookFormDTO GetPrintedBook(string id)
        {
            PrintedBook pb = _printedBookRepository.Get(Guid.Parse(id));
            string? imgPath = null;
            if (File.Exists(Path.Combine(_hostEnvironment.WebRootPath, $"img/printed_books/{id}.jpg")))
                imgPath = $"/img/printed_books/{id}.jpg";

            return new EditBookFormDTO
            {
                Id = pb.Id.ToString(),
                ImagePath = imgPath,
                ISBN = pb.ISBN,
                Amount = pb.AmountLeft,
                Genres = from g in pb.Book.Genres
                         select g.Name,
                Authors = from a in pb.Book.Authors
                          select new AuthorInputModel
                          {
                              Name = a.Name,
                              Surname = a.Surname,
                              Patronymic = a.Patronymic
                          },
                Title = pb.Book.Name,
                PublisherName = pb.Publisher.Name,
                PublishingYear = pb.PublishingYear
            };
        }

        public void RemovePrintedBook(string id)
        {
            PrintedBook pb = _printedBookRepository.Get(Guid.Parse(id));
            _printedBookRepository.Remove(pb);
        }

        public bool IsPrintedBookExist(string id)
        {
            PrintedBook? pb = _printedBookRepository.Get(Guid.Parse(id));
            return pb != null;
        }

        public bool IsPrintedBookInStock(string id)
        {
            PrintedBook? pb = _printedBookRepository.Get(Guid.Parse(id));
            if (pb == null)
                throw new Exception("There is no such printed book in db to check amount in stock");

            return pb.AmountLeft > 0;
        }
    }
}
