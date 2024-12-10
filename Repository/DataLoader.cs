using MassTransit;
using Microsoft.EntityFrameworkCore;
using EL.Domain;
using EL.Repository.Security;

namespace EL.Repository
{
    public class DataLoader
    {
        public static void Load(DbContext context)
        {
            context.Database.EnsureCreated();

            var statuses = new List<Status>();
            if (!context.Set<Status>().Any())
            {
                statuses.Add(new Status { Id = 1, Name = "owner" });
                statuses.Add(new Status { Id = 2, Name = "reader" });
                statuses.Add(new Status { Id = 3, Name = "librarian" });
                context.Set<Status>().AddRange(statuses);
            }

            var users = new List<User>();
            if (!context.Set<User>().Any())
            {
                users.Add(new User
                {
                    Id = NewId.Next().ToGuid(),
                    Name = "Алексей",
                    Surname = "Морозов",
                    Patronymic = "Викторович",
                    BirthDate = new DateOnly(1991, 3, 17),
                    Status = statuses[0],
                    Login = "alex91",
                    Password = PasswordHasher.Hash("owner123"),
                    PhoneNumber = "+71235559393"
                });

                users.Add(new User
                {
                    Id = NewId.Next().ToGuid(),
                    Name = "Марина",
                    Surname = "Кузнецова",
                    Patronymic = "Ивановна",
                    BirthDate = new DateOnly(1985, 7, 22),
                    Status = statuses[2],
                    Login = "marina85",
                    Password = PasswordHasher.Hash("lib123"),
                    PhoneNumber = "+73934885939"
                });

                users.Add(new User
                {
                    Id = NewId.Next().ToGuid(),
                    Name = "Дмитрий",
                    Surname = "Смирнов",
                    Patronymic = "Александрович",
                    BirthDate = new DateOnly(1990, 12, 5),
                    Status = statuses[1],
                    Login = "dmitry90",
                    Password = PasswordHasher.Hash("reader123"),
                    PhoneNumber = "+75934835949"
                });
                //context.Set<Person>().AddRange(users);
                context.Set<User>().AddRange(users);
            }

            var genres = new List<Genre>();
            if (!context.Set<Genre>().Any())
            {
                genres.Add(new Genre { Id = NewId.NextGuid(), Name = "Классическая литература" });
                genres.Add(new Genre { Id = NewId.NextGuid(), Name = "Техническая литература" });
                genres.Add(new Genre { Id = NewId.NextGuid(), Name = "Роман" });
                genres.Add(new Genre { Id = NewId.NextGuid(), Name = "Зарубежная литература" });
                context.Set<Genre>().AddRange(genres);
            }

            var authors = new List<Author>();
            if (!context.Set<Author>().Any())
            {
                authors.Add(new Author { Id = NewId.NextGuid(), Name = "Джек", Surname = "Лондон" });
                authors.Add(new Author { Id = NewId.NextGuid(), Name = "Михаил", Surname = "Лермонтов" });
                authors.Add(new Author { Id = NewId.NextGuid(), Name = "Федор", Surname = "Достоевский" });
                authors.Add(new Author { Id = NewId.NextGuid(), Name = "Герман", Surname = "Гессе" });
                authors.Add(new Author { Id = NewId.NextGuid(), Name = "Роберт", Surname = "Мартин" });
                context.Set<Author>().AddRange(authors);
            }

            var publishers = new List<Publisher>();
            if (!context.Set<Publisher>().Any())
            {
                publishers.Add(new Publisher { Id = NewId.NextGuid(), Name = "Эксмо", CityName = "Санкт-Петербург" });
                publishers.Add(new Publisher { Id = NewId.NextGuid(), Name = "АСТ", CityName = "Москва" });
                publishers.Add(new Publisher { Id = NewId.NextGuid(), Name = "Азбука", CityName = "Москва" });
                context.Set<Publisher>().AddRange(publishers);
            }

            var books = new List<Book>();
            if (!context.Set<Book>().Any())
            {
                books.Add(new Book { Id = NewId.NextGuid(), Name = "Гертруда", Genres = new List<Genre> { genres[0], genres[2], genres[3] }, Authors = new List<Author> { authors[3] }, FirstPublicationYear = 1910 });
                books.Add(new Book { Id = NewId.NextGuid(), Name = "Герой нашего времени", Genres = new List<Genre> { genres[0], genres[2] }, Authors = new List<Author> { authors[1] }, FirstPublicationYear = 1847 });
                books.Add(new Book { Id = NewId.NextGuid(), Name = "Преступление и наказание", Genres = new List<Genre> { genres[0], genres[2] }, Authors = new List<Author> { authors[2] }, FirstPublicationYear = 1866 });
                books.Add(new Book { Id = NewId.NextGuid(), Name = "Чистый код", Genres = new List<Genre> { genres[0], genres[1], genres[3] }, Authors = new List<Author> { authors[4] }, FirstPublicationYear = 2000 });
                books.Add(new Book { Id = NewId.NextGuid(), Name = "Мартин Иден", Genres = new List<Genre> { genres[0], genres[2], genres[3] }, Authors = new List<Author> { authors[0] }, FirstPublicationYear = 1910 });
                books.Add(new Book { Id = NewId.NextGuid(), Name = "Сердца трех", Genres = new List<Genre> { genres[0], genres[2], genres[3] }, Authors = new List<Author> { authors[0] }, FirstPublicationYear = 1918 });
                context.Set<Book>().AddRange(books);
            }


            var printedBooks = new List<PrintedBook>();
            if (!context.Set<PrintedBook>().Any())
            {
                printedBooks.Add(new PrintedBook { Id = Guid.Parse("00130000-2700-0a00-7c70-08dd15c15d86"), ISBN = "978-5-17-095594-7", Publisher = publishers[1], Book = books[0], PublishingYear = 2021, AmountLeft = 1 });
                printedBooks.Add(new PrintedBook { Id = Guid.Parse("00130000-2700-0a00-8bba-08dd15c15d86"), ISBN = "978-5-17-109742-4", Publisher = publishers[1], Book = books[0], PublishingYear = 2022, AmountLeft = 1 });
                printedBooks.Add(new PrintedBook { Id = Guid.Parse("00130000-2700-0a00-8bc2-08dd15c15d86"), ISBN = "978-5-17-092164-5", Publisher = publishers[1], Book = books[1], PublishingYear = 2024, AmountLeft = 1 });
                printedBooks.Add(new PrintedBook { Id = Guid.Parse("00130000-2700-0a00-8bc9-08dd15c15d86"), ISBN = "978-5-389-04926-0", Publisher = publishers[2], Book = books[2], PublishingYear = 2024, AmountLeft = 1 });
                printedBooks.Add(new PrintedBook { Id = Guid.Parse("00130000-2700-0a00-8bcf-08dd15c15d86"), ISBN = "978-5-4461-0069-9", Publisher = publishers[0], Book = books[3], PublishingYear = 2024, AmountLeft = 1 });
                printedBooks.Add(new PrintedBook { Id = Guid.Parse("00130000-2700-0a00-8beb-08dd15c15d86"), ISBN = "978-5-17-087985-4", Publisher = publishers[1], Book = books[4], PublishingYear = 2024, AmountLeft = 1 });
                printedBooks.Add(new PrintedBook { Id = Guid.Parse("00130000-2700-0a00-8bf1-08dd15c15d86"), ISBN = "978-5-17-094715-7", Publisher = publishers[1], Book = books[5], PublishingYear = 2024, AmountLeft = 1 });
                context.Set<PrintedBook>().AddRange(printedBooks);
            }

            try
            {
                context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                var test = ex.Entries;
                Console.WriteLine("j");
            }
        }
    }
}
