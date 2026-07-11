using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Domain.ValueObjects;

public sealed record FullName
{
    public string FirstName { get; }

    public string LastName { get; }

    private FullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public static FullName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("First name cannot be empty.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Last name cannot be empty.");

        firstName = firstName.Trim();
        lastName = lastName.Trim();

        if (firstName.Length > 50)
            throw new DomainException("First name cannot exceed 50 characters.");

        if (lastName.Length > 50)
            throw new DomainException("Last name cannot exceed 50 characters.");

        return new FullName(firstName, lastName);
    }

    public override string ToString()
        => $"{FirstName} {LastName}";
}