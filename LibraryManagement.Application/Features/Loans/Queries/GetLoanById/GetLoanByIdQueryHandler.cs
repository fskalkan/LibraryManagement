using LibraryManagement.Application.Exceptions;
using LibraryManagement.Application.Features.Loans.Responses;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Queries.GetLoanById;

public sealed class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanResponse>
{
    private readonly ILoanRepository _loanRepository;

    public GetLoanByIdQueryHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }

    public async Task<LoanResponse> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(request.Id, cancellationToken);

        if (loan is null)
            throw new NotFoundException(nameof(Loan), request.Id);

        return new LoanResponse(
            loan.Id,
            loan.MemberId,
            loan.BookCopyId,
            loan.BorrowDate,
            loan.DueDate,
            loan.ReturnedDate,
            (int)loan.Status);
    }
}