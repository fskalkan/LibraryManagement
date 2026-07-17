using LibraryManagement.Application.Features.Books.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Queries.GetBookById;

public sealed record GetBookByIdQuery(Guid Id) : IRequest<BookResponse>;