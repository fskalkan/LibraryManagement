using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Domain.Repositories;

public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<Loan>> GetAllAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Loan loan, CancellationToken cancellationToken = default);

    void Update(Loan loan);

    void Remove(Loan loan);

    Task<bool> HasActiveLoanForBookCopyAsync(Guid bookCopyId, CancellationToken cancellationToken = default);
}