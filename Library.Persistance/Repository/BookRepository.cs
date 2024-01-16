using DbDemo;

namespace Library.Persistance.Repository;

public interface IBookRepository : IRepository<Book>;
public sealed class BookRepository : Repository<Book>, IBookRepository;