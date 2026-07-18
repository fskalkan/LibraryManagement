namespace LibraryManagement.Api.Contracts.BookCopies;

public sealed record UpdateBookCopyRequest(
    string Barcode,
    string ShelfLocation);