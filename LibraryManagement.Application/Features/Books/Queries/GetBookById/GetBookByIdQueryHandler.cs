using LibraryManagement.Application.Exceptions;
using LibraryManagement.Application.Features.Books.Responses;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries.GetBookById;

public sealed class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookResponse>
{
    private readonly IBookRepository _bookRepository;

    public GetBookByIdQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<BookResponse> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);

        if (book is null)
            throw new NotFoundException(nameof(Book), request.Id);

        return new BookResponse(
            book.Id,
            book.Title,
            book.Isbn.Value,
            book.PublishYear,
            book.Author.FullName.ToString(),
            book.Category.Name,
            book.Status);
    }
}