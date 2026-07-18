using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.BookCopies.Commands.DeleteBookCopy;

public sealed class DeleteBookCopyCommandHandler : IRequestHandler<DeleteBookCopyCommand>
{
    private readonly IBookCopyRepository _bookCopyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookCopyCommandHandler(
        IBookCopyRepository bookCopyRepository,
        IUnitOfWork unitOfWork)
    {
        _bookCopyRepository = bookCopyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteBookCopyCommand request, CancellationToken cancellationToken)
    {
        var bookCopy = await _bookCopyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (bookCopy is null)
            throw new NotFoundException(nameof(BookCopy), request.Id);

        _bookCopyRepository.Remove(bookCopy);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}