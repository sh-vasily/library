using DbDemo;
using Library.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.Repository;

public interface IBookRepository : IRepository<Book>
{
    Task<List<Book>> GetByNameOrAuthor(string name, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
};
public sealed class BookRepository : Repository<Book>, IBookRepository
{
    public Task<List<Book>> GetByNameOrAuthor(string name, int pageNumber, int pageSize, CancellationToken cancellationToken = default) 
        =>LibraryContext.Set<Book>()
            .Where(book => name
                .Split(" ", StringSplitOptions.TrimEntries)
                .Any(word => book.Title.Contains(word) || book.Author.Contains(word)))
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .OrderBy(book => book.Title)
            .ThenBy(book => book.Author)
            .ToListAsync(cancellationToken);
}