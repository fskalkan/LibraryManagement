namespace LibraryManagement.Api.Contracts.Books;

public sealed record UpdateBookRequest(
    string Title,
    string Isbn,
    int PublishYear,
    Guid AuthorId,
    Guid CategoryId);