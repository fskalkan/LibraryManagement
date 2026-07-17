using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Application.Features.Books.Responses;

public sealed record BookResponse(
    Guid Id,
    string Title,
    string Isbn,
    int PublishYear,
    string Author,
    string Category,
    BookStatus Status);