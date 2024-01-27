using Library.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance;

public sealed class LibraryContext(DbContextOptions<LibraryContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Gender>();
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}