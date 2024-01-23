using DbDemo;
using Library.Persistance.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Library.Persistance;

public sealed class LibraryContext : DbContext
{
    public LibraryContext()
    {
        _connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";
    }

    private string _connectionString { get; }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dataSource = new NpgsqlDataSourceBuilder(_connectionString);
        dataSource.MapEnum<Gender>();
        optionsBuilder.UseNpgsql(dataSource.Build());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Gender>();
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}