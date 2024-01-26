using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.Models;

public class BookInstanceEntiyTypeConfiguration : IEntityTypeConfiguration<BookInstance>
{
    public void Configure(EntityTypeBuilder<BookInstance> builder)
    {
        builder.ToTable("books_instances");
        
        builder
            .Property(s => s.Id)
            .HasColumnName("id")
            .HasDefaultValue(0)
            .IsRequired();
        
        builder
            .Property(s => s.BookId)
            .HasColumnName("book_id")
            .HasDefaultValue(0)
            .IsRequired();
        
        builder
            .HasMany(book => book.BorrowedBooks)
            .WithOne(bookInstance => bookInstance.BookInstance)
            .HasForeignKey(book => book.BookId)
            .HasPrincipalKey(book => book.Id);
    }
}