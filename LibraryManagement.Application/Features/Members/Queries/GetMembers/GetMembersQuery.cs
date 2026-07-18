using LibraryManagement.Application.Features.Members.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Queries.GetMembers;

public sealed record GetMembersQuery : IRequest<IReadOnlyList<MemberResponse>>;