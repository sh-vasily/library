using Microsoft.EntityFrameworkCore;

namespace Library.Data;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
}