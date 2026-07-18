using LibraryManagement.Application.Features.BookCopies.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.BookCopies.Queries.GetBookCopyById;

public sealed record GetBookCopyByIdQuery(Guid Id) : IRequest<BookCopyResponse>;