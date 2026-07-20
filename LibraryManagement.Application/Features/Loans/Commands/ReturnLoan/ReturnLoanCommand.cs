using MediatR;

namespace LibraryManagement.Application.Features.Loans.Commands.ReturnLoan;

public sealed record ReturnLoanCommand(Guid LoanId) : IRequest;