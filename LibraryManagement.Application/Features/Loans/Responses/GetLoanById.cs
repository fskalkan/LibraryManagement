namespace LibraryManagement.Application.Features.Loans.Responses;

public sealed record LoanResponse(
    Guid Id,
    Guid MemberId,
    Guid BookCopyId,
    DateOnly BorrowDate,
    DateOnly DueDate,
    DateOnly? ReturnedDate,
    int Status);