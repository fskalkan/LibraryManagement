using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.DeleteAuthor;

public sealed class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorCommandHandler(
        IAuthorRepository authorRepository,
        IUnitOfWork unitOfWork)
    {
        _authorRepository = authorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (author is null)
            throw new NotFoundException(nameof(Author), request.Id);

        _authorRepository.Remove(author);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}