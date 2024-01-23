using Library.Persistance.Models;

namespace Library.Persistance.Repository;

public interface IBorrowedBooksRepository
{
    Task BorrowBook(int bookId, int userId);
}

public sealed class BorrowedBooksRepository : IBorrowedBooksRepository
{
    private readonly LibraryContext _libraryContext = new();

    public async Task BorrowBook(int bookId, int userId)
    {
        await using var transaction = await _libraryContext.Database.BeginTransactionAsync();
        await _libraryContext.BorrowedBooks.AddAsync(new BorrowedBook()
        {
            BookId = bookId,
            UserId = userId,
        });
        await _libraryContext.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}