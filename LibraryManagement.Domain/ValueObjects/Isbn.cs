using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Domain.ValueObjects;

public sealed record Isbn
{
    public string Value { get; }

    private Isbn(string value)
    {
        Value = value;
    }

    public static Isbn Create(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            throw new DomainException("ISBN cannot be empty.");

        isbn = isbn.Replace("-", "").Trim();

        if (isbn.Length is not (10 or 13))
            throw new DomainException("ISBN must be 10 or 13 characters.");

        return new Isbn(isbn);
    }

    public override string ToString()
        => Value;
}