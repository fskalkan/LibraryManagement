using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Domain.Entities;

public sealed class Category : AggregateRoot
{
    public string Name { get; private set; } = null!;

    private Category(
        Guid id,
        string name)
        : base(id)
    {
        Name = name;
    }

    private Category()
    {
    }

    public static Category Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name cannot be empty.");

        name = name.Trim();

        if (name.Length > 100)
            throw new DomainException("Category name cannot exceed 100 characters.");

        return new Category(
            Guid.NewGuid(),
            name);
    }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name cannot be empty.");

        name = name.Trim();

        if (name.Length > 100)
            throw new DomainException("Category name cannot exceed 100 characters.");

        Name = name;
    }
}