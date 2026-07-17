using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories;

public sealed class BookRepository : IBookRepository
{
    private readonly LibraryDbContext _context;

    public BookRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Books
            .Include(x => x.Author)
            .Include(x => x.Category)
            .Include(x => x.Copies)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Book?> GetByIsbnAsync(Isbn isbn, CancellationToken cancellationToken = default)
    {
        return await _context.Books
            .FirstOrDefaultAsync(x => x.Isbn.Value == isbn.Value, cancellationToken);
    }

    public async Task<bool> ExistsByIsbnAsync(Isbn isbn, CancellationToken cancellationToken = default)
    {
        return await _context.Books
        .AnyAsync(x => x.Isbn.Value == isbn.Value, cancellationToken);
    }

    public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        await _context.Books.AddAsync(book, cancellationToken);
    }

    public void Update(Book book)
    {
        _context.Books.Update(book);
    }

    public void Remove(Book book)
    {
        _context.Books.Remove(book);
    }

    public async Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Books
        .Include(x => x.Author)
        .Include(x => x.Category)
        .AsNoTracking()
        .OrderBy(x => x.Title)
        .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByIsbnExceptBookAsync(Guid bookId, Isbn isbn, CancellationToken cancellationToken = default)
    {
        return await _context.Books.AnyAsync(x =>
            x.Id != bookId &&
            x.Isbn.Value == isbn.Value,
            cancellationToken);
    }
}