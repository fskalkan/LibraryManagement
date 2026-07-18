using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.ValueObjects;

namespace LibraryManagement.Domain.Entities;

public sealed class BookCopy : Entity
{
    public Guid BookId { get; private set; }

    public Book Book { get; private set; } = null!;

    public Barcode Barcode { get; private set; } = null!;

    public string ShelfLocation { get; private set; } = null!;

    public BookCopyStatus Status { get; private set; }

    private BookCopy(
        Guid id,
        Guid bookId,
        Barcode barcode,
        string shelfLocation)
        : base(id)
    {
        BookId = bookId;
        Barcode = barcode;
        ShelfLocation = shelfLocation;
        Status = BookCopyStatus.Available;
    }

    private BookCopy()
    {
    }

    public static BookCopy Create(
        Guid bookId,
        Barcode barcode,
        string shelfLocation)
    {
        if (string.IsNullOrWhiteSpace(shelfLocation))
            throw new DomainException("Shelf location cannot be empty.");

        return new BookCopy(
            Guid.NewGuid(),
            bookId,
            barcode,
            shelfLocation.Trim());
    }

    public void Borrow()
    {
        if (Status != BookCopyStatus.Available)
            throw new DomainException("Book copy is not available.");

        Status = BookCopyStatus.Borrowed;
    }

    public void Return()
    {
        if (Status != BookCopyStatus.Borrowed)
            throw new DomainException("Book copy is not borrowed.");

        Status = BookCopyStatus.Available;
    }

    public void MarkAsLost()
    {
        if (Status == BookCopyStatus.Lost)
            throw new DomainException("Book copy is already marked as lost.");

        Status = BookCopyStatus.Lost;
    }

    public void MarkAsDamaged()
    {
        if (Status == BookCopyStatus.Damaged)
            throw new DomainException("Book copy is already marked as damaged.");

        Status = BookCopyStatus.Damaged;
    }

    public void ChangeShelfLocation(string shelfLocation)
    {
        if (string.IsNullOrWhiteSpace(shelfLocation))
            throw new DomainException("Shelf location cannot be empty.");

        ShelfLocation = shelfLocation.Trim();
    }

    public void ChangeBarcode(Barcode barcode)
    {
        Barcode = barcode;
    }
}