using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Domain.ValueObjects;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.CreateAuthor;

public sealed class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Guid>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var fullName = FullName.Create(
            request.FirstName,
            request.LastName);

        var author = Author.Create(
            fullName,
            request.BirthDate,
            request.Biography);

        await _authorRepository.AddAsync(author, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return author.Id;
    }
}