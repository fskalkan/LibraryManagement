using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Domain.ValueObjects;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands.CreateBook;

public sealed class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookCommandHandler(
        IBookRepository bookRepository,
        IAuthorRepository authorRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var isbn = Isbn.Create(request.Isbn);

        if (await _bookRepository.ExistsByIsbnAsync(isbn, cancellationToken))
            throw new ConflictException("Book with this ISBN already exists.");

        var author = await _authorRepository.GetByIdAsync(request.AuthorId, cancellationToken);

        if (author is null)
            throw new NotFoundException(nameof(Author), request.AuthorId);

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

        if (category is null)
            throw new NotFoundException(nameof(Category), request.CategoryId);

        var book = Book.Create(
            request.Title,
            isbn,
            request.PublishYear,
            request.AuthorId,
            request.CategoryId);

        await _bookRepository.AddAsync(book, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}