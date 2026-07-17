using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories;

public sealed class BookCopyRepository : IBookCopyRepository
{
    private readonly LibraryDbContext _context;

    public BookCopyRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<BookCopy?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.BookCopies
            .Include(x => x.Book)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BookCopy>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.BookCopies
            .Include(x => x.Book)
            .AsNoTracking()
            .OrderBy(x => x.Barcode.Value)
            .ToListAsync(cancellationToken);
    }

    public async Task<BookCopy?> GetByBarcodeAsync(Barcode barcode, CancellationToken cancellationToken = default)
    {
        return await _context.BookCopies
            .FirstOrDefaultAsync(x => x.Barcode.Value == barcode.Value, cancellationToken);
    }

    public async Task<bool> ExistsByBarcodeAsync(Barcode barcode, CancellationToken cancellationToken = default)
    {
        return await _context.BookCopies
            .AnyAsync(x => x.Barcode.Value == barcode.Value, cancellationToken);
    }

    public async Task<bool> ExistsByBarcodeExceptBookCopyAsync(Guid bookCopyId, Barcode barcode, CancellationToken cancellationToken = default)
    {
        return await _context.BookCopies
            .AnyAsync(x =>
                x.Id != bookCopyId &&
                x.Barcode.Value == barcode.Value,
                cancellationToken);
    }

    public async Task AddAsync(BookCopy bookCopy, CancellationToken cancellationToken = default)
    {
        await _context.BookCopies.AddAsync(bookCopy, cancellationToken);
    }

    public void Update(BookCopy bookCopy)
    {
        _context.BookCopies.Update(bookCopy);
    }

    public void Remove(BookCopy bookCopy)
    {
        _context.BookCopies.Remove(bookCopy);
    }
}