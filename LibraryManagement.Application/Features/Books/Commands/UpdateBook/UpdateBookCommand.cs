using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands.UpdateBook;

public sealed record UpdateBookCommand(
    Guid Id,
    string Title,
    string Isbn,
    int PublishYear,
    Guid AuthorId,
    Guid CategoryId) : IRequest;