using MediatR;

namespace LibraryManagement.Application.Features.Loans.Commands.CreateLoan;

public sealed record CreateLoanCommand(
    Guid MemberId,
    Guid BookCopyId,
    DateOnly BorrowDate,
    DateOnly DueDate) : IRequest<Guid>;