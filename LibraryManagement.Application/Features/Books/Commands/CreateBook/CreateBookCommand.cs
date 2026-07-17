using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands.CreateBook;

public sealed record CreateBookCommand(
    string Title,
    string Isbn,
    int PublishYear,
    Guid AuthorId,
    Guid CategoryId) : IRequest<Guid>;