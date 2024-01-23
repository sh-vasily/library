﻿using Library.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.Repository;

public interface IBorrowedBooksRepository : IRepository<BorrowedBook>
{
    Task BorrowBook(int bookId, int userId);
    Task<int> GetBorrowedBooksCount(int bookId);
}

public sealed class BorrowedBooksRepository : Repository<BorrowedBook>, IBorrowedBooksRepository
{
    public async Task BorrowBook(int bookId, int userId)
    {
        await using var transaction = await LibraryContext.Database.BeginTransactionAsync();
        await LibraryContext.BorrowedBooks.AddAsync(new BorrowedBook()
        {
            BookId = bookId,
            UserId = userId,
            BorrowDate = DateTime.UtcNow
        });
        await LibraryContext.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public Task<int> GetBorrowedBooksCount(int bookId) =>
        LibraryContext.BorrowedBooks
            .Where(book => book.BookId == bookId )
            .CountAsync();
}