using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Commands.CreateLoan;

public sealed class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Guid>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IBookCopyRepository _bookCopyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateLoanCommandHandler(
        ILoanRepository loanRepository,
        IMemberRepository memberRepository,
        IBookCopyRepository bookCopyRepository,
        IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _memberRepository = memberRepository;
        _bookCopyRepository = bookCopyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.MemberId, cancellationToken);

        if (member is null)
            throw new NotFoundException(nameof(Member), request.MemberId);

        var bookCopy = await _bookCopyRepository.GetByIdAsync(request.BookCopyId, cancellationToken);

        if (bookCopy is null)
            throw new NotFoundException(nameof(BookCopy), request.BookCopyId);

        if (await _loanRepository.HasActiveLoanForBookCopyAsync(request.BookCopyId, cancellationToken))
            throw new ConflictException("Book copy is already on loan.");

        var loan = Loan.Create(
            request.MemberId,
            request.BookCopyId,
            request.BorrowDate,
            request.DueDate);

        bookCopy.Borrow();

        await _loanRepository.AddAsync(loan, cancellationToken);

        _bookCopyRepository.Update(bookCopy);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return loan.Id;
    }
}