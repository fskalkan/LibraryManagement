using LibraryManagement.Application.Features.BookCopies.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.BookCopies.Queries.GetBookCopies;

public sealed record GetBookCopiesQuery() : IRequest<IReadOnlyList<BookCopyResponse>>;