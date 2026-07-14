using LibraryManagement.Application.Exceptions;
using LibraryManagement.Application.Features.Authors.Responses;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Queries.GetAuthorById;

public sealed class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorResponse>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorByIdQueryHandler(
        IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<AuthorResponse> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (author is null)
            throw new NotFoundException(nameof(Author), request.Id);

        return new AuthorResponse(
            author.Id,
            author.FullName.FirstName,
            author.FullName.LastName,
            author.BirthDate,
            author.Biography);
    }
}