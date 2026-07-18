using LibraryManagement.Application.Features.Members.Responses;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Queries.GetMembers;

public sealed class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IReadOnlyList<MemberResponse>>
{
    private readonly IMemberRepository _memberRepository;

    public GetMembersQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<IReadOnlyList<MemberResponse>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
    {
        var members = await _memberRepository.GetAllAsync(cancellationToken);

        return members
            .Select(member => new MemberResponse(
                member.Id,
                member.FullName.FirstName,
                member.FullName.LastName,
                member.Email.Value,
                member.Status))
            .ToList();
    }
}