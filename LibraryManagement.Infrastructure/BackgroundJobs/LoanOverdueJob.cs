using LibraryManagement.Domain.Repositories;

namespace LibraryManagement.Infrastructure.BackgroundJobs;

public sealed class LoanOverdueJob
{
    private readonly ILoanRepository _loanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LoanOverdueJob(
        ILoanRepository loanRepository,
        IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync()
    {
        var overdueLoans = await _loanRepository.GetOverdueLoansAsync();

        foreach (var loan in overdueLoans)
        {
            loan.MarkAsOverdue();

            _loanRepository.Update(loan);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}