using MediatR;

public sealed record CreateBookCopyCommand(
    Guid BookId,
    string Barcode,
    string ShelfLocation)
    : IRequest<Guid>;