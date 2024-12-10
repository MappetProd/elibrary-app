using EL.Domain;


namespace EL.Repository.Contracts
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Book? GetBook(string title, IEnumerable<Author> authors);
    }
}
