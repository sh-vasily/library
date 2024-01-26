using Library.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.Repository;

public interface IBookRepository : IRepository<Book>
{
    Task<List<Book>> GetByNameOrAuthor(string name, int pageNumber, int pageSize, CancellationToken cancellationToken = default);

    Task<BookWithInstances?> GetBookWithInstances(int id);
};
public sealed class BookRepository : Repository<Book>, IBookRepository
{
    public Task<List<Book>> GetByNameOrAuthor(string name, int pageNumber, int pageSize, CancellationToken cancellationToken = default) 
        => LibraryContext.Set<Book>()
            .Where(book => name
                .Split(" ", StringSplitOptions.TrimEntries)
                .Any(word => book.Title.Contains(word) || book.Author.Contains(word)))
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .OrderBy(book => book.Title)
            .ThenBy(book => book.Author)
            .ToListAsync(cancellationToken);

    public async Task<BookWithInstances?> GetBookWithInstances(int id)
     => await LibraryContext.Set<Book>()
            .Include(book => book.BookInstances)
            .Where(book => book.Id == id)
            .Select(book => new BookWithInstances
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Isbn = book.Isbn,
                AllCount = book.BookInstances.Count,
                AvailableCount = book.BookInstances
                    .Count(instance => instance.BorrowedBooks
                        .All(borrowedBook => borrowedBook.ReturnDate != null))
            })
            .FirstAsync();
}