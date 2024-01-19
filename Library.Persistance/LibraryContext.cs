using DbDemo;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dataSource = new NpgsqlDataSourceBuilder(_connectionString);
        dataSource.MapEnum<Gender>();
        optionsBuilder.UseNpgsql(dataSource.Build());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Gender>();
        modelBuilder.Entity<Book>()
            .Property(s => s.Id)
            .HasColumnName("id")
            .HasDefaultValue(0)
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(s => s.Author)
            .HasColumnName("author")
            .IsRequired();

        modelBuilder.Entity<Book>()
            .Property(s => s.Title)
            .HasColumnName("title")
            .IsRequired();
        
        modelBuilder.Entity<Book>()
            .Property(s => s.Isbn)
            .HasColumnName("isbn")
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(s => s.Id)
            .HasColumnName("id")
            .HasDefaultValue(0)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(s => s.FirstName)
            .HasColumnName("firstname")
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(s => s.LastName)
            .HasColumnName("lastname")
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(s => s.Birthday)
            .HasColumnName("birthday")
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(s => s.Address)
            .HasColumnName("address")
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(s => s.Gender)
            .HasColumnName("gender")
            .HasConversion<Gender>()
            .IsRequired();
    }
}