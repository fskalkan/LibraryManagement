using LibraryManagement.Application.Features.Books.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries.GetBooks;

public sealed record GetBooksQuery : IRequest<IReadOnlyList<BookResponse>>;