using DbDemo;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.Repository;

public interface IRepository<T>
{
    Task<List<T>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly LibraryContext _libraryContext = new();

    public Task<List<T>> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return _libraryContext.Set<T>()
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

}