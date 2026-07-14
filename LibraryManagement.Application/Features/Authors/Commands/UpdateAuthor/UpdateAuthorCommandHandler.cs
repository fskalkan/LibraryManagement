using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Domain.ValueObjects;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.UpdateAuthor;

public sealed class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (author is null)
            throw new NotFoundException(nameof(Author), request.Id);

        author.ChangeName(
            FullName.Create(
                request.FirstName,
                request.LastName));

        author.ChangeBiography(request.Biography);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}