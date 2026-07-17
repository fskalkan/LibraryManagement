using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Domain.ValueObjects;
using MediatR;

namespace LibraryManagement.Application.Features.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(
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

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);

        if (book is null)
            throw new NotFoundException(nameof(Book), request.Id);

        var isbn = Isbn.Create(request.Isbn);

        if (await _bookRepository.ExistsByIsbnExceptBookAsync(request.Id, isbn, cancellationToken))
        {
            throw new ConflictException("Book with this ISBN already exists.");
        }

        var author = await _authorRepository.GetByIdAsync(request.AuthorId, cancellationToken);

        if (author is null)
            throw new NotFoundException(nameof(Author), request.AuthorId);

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);

        if (category is null)
            throw new NotFoundException(nameof(Category), request.CategoryId);

        book.ChangeTitle(request.Title);
        book.ChangeIsbn(isbn);
        book.ChangePublishYear(request.PublishYear);
        book.ChangeAuthor(request.AuthorId);
        book.ChangeCategory(request.CategoryId);

        _bookRepository.Update(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}