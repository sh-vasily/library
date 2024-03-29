﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.Models;

public sealed class BorrowedBookEntityTypeConfiguration : IEntityTypeConfiguration<BorrowedBook>
{
    public void Configure(EntityTypeBuilder<BorrowedBook> builder)
    {
        builder.ToTable("borrowed_books");

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
            .Property(s => s.UserId)
            .HasColumnName("user_id")
            .HasDefaultValue(0)
            .IsRequired();
        
        builder
            .Property(s => s.BorrowDate)
            .HasColumnName("borrow_date")
            .IsRequired();
        
        builder
            .Property(s => s.ReturnDate)
            .HasColumnName("return_date");
    }
}