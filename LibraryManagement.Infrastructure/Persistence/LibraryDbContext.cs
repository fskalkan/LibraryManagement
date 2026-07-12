using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence;

public sealed class LibraryDbContext : DbContext, IUnitOfWork
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }

    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Book> Books => Set<Book>();

    public DbSet<BookCopy> BookCopies => Set<BookCopy>();

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Member> Members => Set<Member>();

    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
    }
}