using FluentValidation;

namespace LibraryManagement.Application.Features.Loans.Commands.ReturnLoan;

public sealed class ReturnLoanCommandValidator : AbstractValidator<ReturnLoanCommand>
{
    public ReturnLoanCommandValidator()
    {
        RuleFor(x => x.LoanId)
            .NotEmpty();
    }
}