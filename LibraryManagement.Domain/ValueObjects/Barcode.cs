using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Domain.ValueObjects;

public sealed record Barcode
{
    public string Value { get; }

    private Barcode(string value)
    {
        Value = value;
    }

    public static Barcode Create(string barcode)
    {
        if (string.IsNullOrWhiteSpace(barcode))
            throw new DomainException("Barcode cannot be empty.");

        barcode = barcode.Trim();

        if (barcode.Length > 50)
            throw new DomainException("Barcode cannot exceed 50 characters.");

        return new Barcode(barcode);
    }

    public override string ToString()
        => Value;
}