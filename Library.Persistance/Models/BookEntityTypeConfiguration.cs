using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.Models;

public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("books");
        builder
            .Property(s => s.Id)
            .HasColumnName("id")
            .HasDefaultValue(0)
            .IsRequired();

        builder
            .Property(s => s.Author)
            .HasColumnName("author")
            .IsRequired();

        builder
            .Property(s => s.Title)
            .HasColumnName("title")
            .IsRequired();
        
        builder
            .Property(s => s.Isbn)
            .HasColumnName("isbn")
            .IsRequired();
    }
}