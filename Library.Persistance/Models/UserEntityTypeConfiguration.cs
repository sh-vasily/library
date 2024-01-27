using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.Models;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder
            .Property(s => s.Id)
            .HasColumnName("id")
            .HasDefaultValue(0)
            .IsRequired();
        
        builder
            .Property(s => s.FirstName)
            .HasColumnName("first_name")
            .IsRequired();
        
        builder
            .Property(s => s.LastName)
            .HasColumnName("last_name")
            .IsRequired();
        
        builder
            .Property(s => s.Birthday)
            .HasColumnName("birthday")
            .IsRequired();
        
        builder
            .Property(s => s.Address)
            .HasColumnName("address")
            .IsRequired();
        
        builder
            .Property(s => s.Gender)
            .HasColumnName("gender")
            .HasConversion<Gender>()
            .IsRequired();
    }
}