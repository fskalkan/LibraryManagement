using LibraryManagement.Application.Features.Loans.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Queries.GetLoanById;

public sealed record GetLoanByIdQuery(Guid Id) : IRequest<LoanResponse>;