using MediatR;

namespace LibraryManagement.Application.Features.BookCopies.Commands.DeleteBookCopy;

public sealed record DeleteBookCopyCommand(Guid Id) : IRequest;