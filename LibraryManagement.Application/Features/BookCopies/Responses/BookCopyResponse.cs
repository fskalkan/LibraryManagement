using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Application.Features.BookCopies.Responses;

public sealed record BookCopyResponse(
    Guid Id,
    Guid BookId,
    string BookTitle,
    string Barcode,
    string ShelfLocation,
    BookCopyStatus Status);