using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.ValueObjects;

namespace LibraryManagement.Domain.Repositories;

public interface IBookCopyRepository
{
    Task<BookCopy?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BookCopy>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<BookCopy?> GetByBarcodeAsync(Barcode barcode, CancellationToken cancellationToken = default);

    Task<bool> ExistsByBarcodeAsync(Barcode barcode, CancellationToken cancellationToken = default);

    Task<bool> ExistsByBarcodeExceptBookCopyAsync(Guid bookCopyId, Barcode barcode, CancellationToken cancellationToken = default);

    Task AddAsync(BookCopy bookCopy, CancellationToken cancellationToken = default);

    void Update(BookCopy bookCopy);

    void Remove(BookCopy bookCopy);
}
