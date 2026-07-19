using FluentValidation;

namespace LibraryManagement.Application.Features.Loans.Commands.CreateLoan;

public sealed class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
{
    public CreateLoanCommandValidator()
    {
        RuleFor(x => x.MemberId)
            .NotEmpty();

        RuleFor(x => x.BookCopyId)
            .NotEmpty();

        RuleFor(x => x.BorrowDate)
            .NotEmpty();

        RuleFor(x => x.DueDate)
            .GreaterThan(x => x.BorrowDate);
    }
}