using LibraryManagement.Application.Features.Loans.Responses;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Queries.GetLoans;

public sealed class GetLoansQueryHandler : IRequestHandler<GetLoansQuery, IReadOnlyList<LoanResponse>>
{
    private readonly ILoanRepository _loanRepository;

    public GetLoansQueryHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<IReadOnlyList<LoanResponse>> Handle(GetLoansQuery request, CancellationToken cancellationToken)
    {
        var loans = await _loanRepository.GetAllAsync(cancellationToken);

        return loans.Select(x => new LoanResponse(
                x.Id,
                x.MemberId,
                x.BookCopyId,
                x.BorrowDate,
                x.DueDate,
                x.ReturnedDate,
                (int)x.Status))
            .ToList();
    }
}