using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories;

public sealed class MemberRepository : IMemberRepository
{
    private readonly LibraryDbContext _context;

    public MemberRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Members
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Member?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await _context.Members
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return await _context.Members
            .AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task AddAsync(Member member, CancellationToken cancellationToken = default)
    {
        await _context.Members.AddAsync(member, cancellationToken);
    }

    public void Update(Member member)
    {
        _context.Members.Update(member);
    }

    public void Remove(Member member)
    {
        _context.Members.Remove(member);
    }
}