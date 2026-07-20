using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Commands.ReturnLoan;

public sealed class ReturnLoanCommandHandler : IRequestHandler<ReturnLoanCommand>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookCopyRepository _bookCopyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReturnLoanCommandHandler(
        ILoanRepository loanRepository,
        IBookCopyRepository bookCopyRepository,
        IUnitOfWork unitOfWork)
    {
        _loanRepository = loanRepository;
        _bookCopyRepository = bookCopyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ReturnLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(request.LoanId, cancellationToken);

        if (loan is null)
            throw new NotFoundException(nameof(Loan), request.LoanId);

        var bookCopy = await _bookCopyRepository.GetByIdAsync(loan.BookCopyId, cancellationToken);

        if (bookCopy is null)
            throw new NotFoundException(nameof(BookCopy), loan.BookCopyId);

        loan.Return();

        bookCopy.Return();

        _loanRepository.Update(loan);
        _bookCopyRepository.Update(bookCopy);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}