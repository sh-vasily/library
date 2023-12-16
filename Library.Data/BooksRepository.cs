namespace Library.Data;

public interface IBookRepository
{
    public List<Book> GetAll();
}

public class BooksRepository : IBookRepository
{
    public List<Book> GetAll()
    {
        return ListUtils.ListOf(new Book { Title = "Война и мир" });
    }
}