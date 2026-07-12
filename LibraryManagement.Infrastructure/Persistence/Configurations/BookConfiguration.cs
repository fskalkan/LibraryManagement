using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Persistence.Configurations;

public sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.PublishYear)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>();

        builder.OwnsOne(x => x.Isbn, isbn =>
        {
            isbn.Property(x => x.Value)
                .HasColumnName("Isbn")
                .HasMaxLength(13)
                .IsRequired();
        });

        builder.HasMany(x => x.Copies)
            .WithOne()
            .HasForeignKey("BookId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}