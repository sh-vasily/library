using DbDemo;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.Repository;

public interface IBookRepository : IRepository<Book>
{
    Task<List<Book>> GetByName(string name, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
};
public sealed class BookRepository : Repository<Book>, IBookRepository
{
    public Task<List<Book>> GetByName(string name, int pageNumber, int pageSize, CancellationToken cancellationToken = default) 
        =>_libraryContext.Set<Book>()
            .Where(book => book.Title.Contains(name))
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
}