using LibraryManagement.Application.Features.Authors.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Queries.GetAuthorById;

public sealed record GetAuthorByIdQuery(Guid Id)
    : IRequest<AuthorResponse>;