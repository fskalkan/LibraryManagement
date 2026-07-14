using LibraryManagement.Application.Features.Authors.Responses;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Queries.GetAuthors;

public sealed class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IReadOnlyList<AuthorResponse>>
{
    private readonly IAuthorRepository _authorRepository;

    public GetAuthorsQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<IReadOnlyList<AuthorResponse>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAsync(cancellationToken);

        return authors
            .Select(author => new AuthorResponse(
                author.Id,
                author.FullName.FirstName,
                author.FullName.LastName,
                author.BirthDate,
                author.Biography))
            .ToList();
    }
}