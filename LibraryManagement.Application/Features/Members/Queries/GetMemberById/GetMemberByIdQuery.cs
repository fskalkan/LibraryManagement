using LibraryManagement.Application.Features.Members.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Queries.GetMemberById;

public sealed record GetMemberByIdQuery(Guid Id) : IRequest<MemberResponse>;