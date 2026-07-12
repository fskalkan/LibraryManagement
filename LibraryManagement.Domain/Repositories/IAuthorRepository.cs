using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.ValueObjects;

namespace LibraryManagement.Domain.Repositories;

public interface IAuthorRepository
{
    Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Author>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Author author, CancellationToken cancellationToken = default);

    void Update(Author author);

    void Remove(Author author);
}