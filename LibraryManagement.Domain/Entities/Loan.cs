using LibraryManagement.Domain.Common;
using LibraryManagement.Domain.Enums;
using LibraryManagement.Domain.Exceptions;

namespace LibraryManagement.Domain.Entities;

public sealed class Loan : AggregateRoot
{
    public Guid MemberId { get; private set; }

    public Guid BookCopyId { get; private set; }

    public DateOnly BorrowDate { get; private set; }

    public DateOnly DueDate { get; private set; }

    public DateOnly? ReturnedDate { get; private set; }

    public LoanStatus Status { get; private set; }

    private Loan(
        Guid id,
        Guid memberId,
        Guid bookCopyId,
        DateOnly borrowDate,
        DateOnly dueDate)
        : base(id)
    {
        MemberId = memberId;
        BookCopyId = bookCopyId;
        BorrowDate = borrowDate;
        DueDate = dueDate;
        Status = LoanStatus.Active;
    }

    protected Loan()
    {
    }

    public static Loan Create(
        Guid memberId,
        Guid bookCopyId,
        DateOnly borrowDate,
        DateOnly dueDate)
    {
        if (dueDate <= borrowDate)
            throw new DomainException("Due date must be after borrow date.");

        return new Loan(
            Guid.NewGuid(),
            memberId,
            bookCopyId,
            borrowDate,
            dueDate);
    }

    public void Return()
    {
        if (Status == LoanStatus.Returned)
            throw new DomainException("Loan has already been returned.");

        ReturnedDate = DateOnly.FromDateTime(DateTime.UtcNow);
        Status = LoanStatus.Returned;
    }

    public void MarkAsOverdue()
    {
        if (Status != LoanStatus.Active)
            throw new DomainException("Only active loans can become overdue.");

        Status = LoanStatus.Overdue;
    }
}