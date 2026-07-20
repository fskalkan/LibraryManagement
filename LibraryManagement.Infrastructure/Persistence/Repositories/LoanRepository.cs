using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence.Repositories;

public sealed class LoanRepository : ILoanRepository
{
    private readonly LibraryDbContext _context;

    public LoanRepository(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Loans
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Loan>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Loans
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }


    public async Task AddAsync(Loan loan, CancellationToken cancellationToken = default)
    {
        await _context.Loans.AddAsync(loan, cancellationToken);
    }

    public void Update(Loan loan)
    {
        _context.Loans.Update(loan);
    }

    public void Remove(Loan loan)
    {
        _context.Loans.Remove(loan);
    }

    public async Task<bool> HasActiveLoanForBookCopyAsync(Guid bookCopyId, CancellationToken cancellationToken = default)
    {
        return await _context.Loans.AnyAsync(
            x => x.BookCopyId == bookCopyId &&
                 x.Status == LoanStatus.Active,
            cancellationToken);
    }
}