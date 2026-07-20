using LibraryManagement.Application.Features.Loans.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.Loans.Queries.GetLoans;

public sealed record GetLoansQuery : IRequest<IReadOnlyList<LoanResponse>>;