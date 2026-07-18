using MediatR;

namespace LibraryManagement.Application.Features.BookCopies.Commands.UpdateBookCopy;

public sealed record UpdateBookCopyCommand(
    Guid Id,
    string Barcode,
    string ShelfLocation)
    : IRequest;