using LibraryManagement.Application.Features.Authors.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Queries.GetAuthors;

public sealed record GetAuthorsQuery
    : IRequest<IReadOnlyList<AuthorResponse>>;