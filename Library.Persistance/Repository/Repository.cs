using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.Repository;

public interface IRepository<T>
{
    Task<List<T>> GetAll();
}

public abstract class Repository<T>(LibraryContext libraryContext) : IRepository<T>
    where T : class
{
    protected readonly LibraryContext LibraryContext = libraryContext;
    public Task<List<T>> GetAll() => LibraryContext.Set<T>().ToListAsync();
}