using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.Repository;

public interface IRepository<T>
{
    ValueTask<T?> GetById(int id);
    Task<List<T>> GetAll();
}

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly LibraryContext LibraryContext = new();

    public Task<List<T>> GetAll() => LibraryContext.Set<T>().ToListAsync();
    public ValueTask<T?> GetById(int id) => LibraryContext.FindAsync<T>(id);
}