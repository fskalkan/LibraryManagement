using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Domain.ValueObjects;
using MediatR;

namespace LibraryManagement.Application.Features.BookCopies.Commands.UpdateBookCopy;

public sealed class UpdateBookCopyCommandHandler : IRequestHandler<UpdateBookCopyCommand>
{
    private readonly IBookCopyRepository _bookCopyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCopyCommandHandler(
        IBookCopyRepository bookCopyRepository,
        IUnitOfWork unitOfWork)
    {
        _bookCopyRepository = bookCopyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateBookCopyCommand request, CancellationToken cancellationToken)
    {
        var bookCopy = await _bookCopyRepository.GetByIdAsync(request.Id, cancellationToken);

        if (bookCopy is null)
            throw new NotFoundException(nameof(BookCopy), request.Id);

        var barcode = Barcode.Create(request.Barcode);

        if (await _bookCopyRepository.ExistsByBarcodeExceptBookCopyAsync(request.Id, barcode, cancellationToken))
        {
            throw new ConflictException($"Book copy with barcode '{barcode.Value}' already exists.");
        }

        bookCopy.ChangeBarcode(barcode);
        bookCopy.ChangeShelfLocation(request.ShelfLocation);

        _bookCopyRepository.Update(bookCopy);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}