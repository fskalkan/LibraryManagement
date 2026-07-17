using System.Linq;
using LibraryManagement.Application.Features.Books.Responses;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries.GetBooks;

public sealed class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IReadOnlyList<BookResponse>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IReadOnlyList<BookResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync(cancellationToken);

        return books
            .Select(book => new BookResponse(
                book.Id,
                book.Title,
                book.Isbn.Value,
                book.PublishYear,
                book.Author.FullName.ToString(),
                book.Category.Name,
                book.Status))
            .ToList();
    }
}