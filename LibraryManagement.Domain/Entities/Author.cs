using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Exceptions;
using LibraryManagement.Domain.ValueObjects;

namespace LibraryManagement.Domain.Entities;

public sealed class Author : AggregateRoot
{
    public FullName FullName { get; private set; } = null!;

    public DateOnly BirthDate { get; private set; }

    public string? Biography { get; private set; } = null!;

    private Author(
        Guid id,
        FullName fullName,
        DateOnly birthDate,
        string? biography)
        : base(id)
    {
        FullName = fullName;
        BirthDate = birthDate;
        Biography = biography;
    }

    private Author()
    {
    }

    public static Author Create(
        FullName fullName,
        DateOnly birthDate,
        string? biography)
    {
        if (birthDate > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new DomainException("Birth date cannot be in the future.");

        return new Author(
            Guid.NewGuid(),
            fullName,
            birthDate,
            biography?.Trim());
    }

    public void ChangeName(FullName fullName)
    {
        FullName = fullName;
    }

    public void ChangeBiography(string? biography)
    {
        Biography = biography?.Trim();
    }
}