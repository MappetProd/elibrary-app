using EL.Domain;
using EL.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EL.Repository.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(ElibraryContext context) : base(context) { }

        public Book? GetBook(string title, IEnumerable<Author> authors)
        {
            List<Book> books = (from b in entities
                                where b.Name == title
                                select b).ToList();

            foreach (Book book in books)
            {
                foreach (Author a in book.Authors)
                {
                    if (!authors.Contains(a))
                        books.Remove(book);
                }
            }

            if (books.Count == 0)
                return null;

            //todo smth when there are two same books
            return books[0];
        }
    }
}
