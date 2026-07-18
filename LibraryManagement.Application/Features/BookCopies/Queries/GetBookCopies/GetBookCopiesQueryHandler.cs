using LibraryManagement.Application.Features.BookCopies.Responses;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.BookCopies.Queries.GetBookCopies;

public sealed class GetBookCopiesQueryHandler : IRequestHandler<GetBookCopiesQuery, IReadOnlyList<BookCopyResponse>>
{
    private readonly IBookCopyRepository _bookCopyRepository;

    public GetBookCopiesQueryHandler(IBookCopyRepository bookCopyRepository)
    {
        _bookCopyRepository = bookCopyRepository;
    }

    public async Task<IReadOnlyList<BookCopyResponse>> Handle(GetBookCopiesQuery request, CancellationToken cancellationToken)
    {
        var bookCopies = await _bookCopyRepository.GetAllAsync(cancellationToken);

        return bookCopies
            .Select(bookCopy => new BookCopyResponse(
                bookCopy.Id,
                bookCopy.BookId,
                bookCopy.Book.Title,
                bookCopy.Barcode.Value,
                bookCopy.ShelfLocation,
                bookCopy.Status))
            .ToList();
    }
}