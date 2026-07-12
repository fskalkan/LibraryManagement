using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories;

public sealed class AuthorRepository : IAuthorRepository
{
    private readonly LibraryDbContext _context;
    public AuthorRepository(LibraryDbContext context)
    {
        _context = context;
    }
    public async Task<Author?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Authors
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<IReadOnlyList<Author>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Authors
            .OrderBy(x => x.FullName.LastName)
            .ThenBy(x => x.FullName.FirstName)
            .ToListAsync(cancellationToken);
    }
    public async Task AddAsync(Author author, CancellationToken cancellationToken = default)
    {
        await _context.Authors.AddAsync(author, cancellationToken);
    }
    public void Update(Author author)
    {
        _context.Authors.Update(author);
    }
    public void Remove(Author author)
    {
        _context.Authors.Remove(author);
    }
}

