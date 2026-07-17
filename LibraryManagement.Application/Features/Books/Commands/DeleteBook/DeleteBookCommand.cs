using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands.DeleteBook;

public sealed record DeleteBookCommand(Guid Id) : IRequest;