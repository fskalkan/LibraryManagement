using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.ValueObjects;

namespace LibraryManagement.Domain.Entities;

public sealed class Book : AggregateRoot
{
    private readonly List<BookCopy> _copies = [];

    public string Title { get; private set; } = null!;

    public Isbn Isbn { get; private set; } = null!;

    public int PublishYear { get; private set; }

    public Guid AuthorId { get; private set; }

    public Author Author { get; private set; } = null!;

    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; } = null!;

    public BookStatus Status { get; private set; }

    public IReadOnlyCollection<BookCopy> Copies => _copies.AsReadOnly();

    private Book(
        Guid id,
        string title,
        Isbn isbn,
        int publishYear,
        Guid authorId,
        Guid categoryId)
        : base(id)
    {
        Title = title;
        Isbn = isbn;
        PublishYear = publishYear;
        AuthorId = authorId;
        CategoryId = categoryId;
        Status = BookStatus.Available;
    }

    private Book()
    {
    }

    public static Book Create(
        string title,
        Isbn isbn,
        int publishYear,
        Guid authorId,
        Guid categoryId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Title cannot be empty.");

        if (publishYear > DateTime.UtcNow.Year)
            throw new DomainException("Publish year cannot be in the future.");

        return new Book(
            Guid.NewGuid(),
            title.Trim(),
            isbn,
            publishYear,
            authorId,
            categoryId);
    }

    public void ChangeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Title cannot be empty.");

        Title = title.Trim();
    }

    public void ChangeIsbn(Isbn isbn)
    {
        Isbn = isbn;
    }

    public void AddCopy(BookCopy copy)
    {
        _copies.Add(copy);

        Status = BookStatus.Available;
    }

    public void RemoveCopy(BookCopy copy)
    {
        _copies.Remove(copy);

        if (_copies.Count == 0)
            Status = BookStatus.Unavailable;
    }

    public void ChangePublishYear(int publishYear)
    {
        if (publishYear > DateTime.UtcNow.Year)
            throw new DomainException("Publish year cannot be in the future.");

        PublishYear = publishYear;
    }

    public void ChangeAuthor(Guid authorId)
    {
        AuthorId = authorId;
    }

    public void ChangeCategory(Guid categoryId)
    {
        CategoryId = categoryId;
    }
}