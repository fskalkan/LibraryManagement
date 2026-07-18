using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Domain.ValueObjects;
using MediatR;

namespace LibraryManagement.Application.Features.BookCopies.Commands.CreateBookCopy;

public sealed class CreateBookCopyCommandHandler : IRequestHandler<CreateBookCopyCommand, Guid>
{
    private readonly IBookRepository _bookRepository;
    private readonly IBookCopyRepository _bookCopyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookCopyCommandHandler(
        IBookRepository bookRepository,
        IBookCopyRepository bookCopyRepository,
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _bookCopyRepository = bookCopyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateBookCopyCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId, cancellationToken);

        if (book is null)
            throw new NotFoundException(nameof(Book), request.BookId);

        var barcode = Barcode.Create(request.Barcode);

        if (await _bookCopyRepository.ExistsByBarcodeAsync(barcode, cancellationToken))
        {
            throw new ConflictException($"Book copy with barcode '{barcode.Value}' already exists.");
        }

        var bookCopy = BookCopy.Create(
            request.BookId,
            barcode,
            request.ShelfLocation);

        await _bookCopyRepository.AddAsync(bookCopy, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return bookCopy.Id;
    }
}