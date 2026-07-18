using LibraryManagement.Application.Exceptions;
using LibraryManagement.Application.Features.BookCopies.Responses;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.BookCopies.Queries.GetBookCopyById;

public sealed class GetBookCopyByIdQueryHandler : IRequestHandler<GetBookCopyByIdQuery, BookCopyResponse>
{
    private readonly IBookCopyRepository _bookCopyRepository;

    public GetBookCopyByIdQueryHandler(IBookCopyRepository bookCopyRepository)
    {
        _bookCopyRepository = bookCopyRepository;
    }

    public async Task<BookCopyResponse> Handle(GetBookCopyByIdQuery request, CancellationToken cancellationToken)
    {
        var bookCopy = await _bookCopyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (bookCopy is null)
            throw new NotFoundException(nameof(BookCopy), request.Id);

        return new BookCopyResponse(
            bookCopy.Id,
            bookCopy.BookId,
            bookCopy.Book.Title,
            bookCopy.Barcode.Value,
            bookCopy.ShelfLocation,
            bookCopy.Status);
    }
}