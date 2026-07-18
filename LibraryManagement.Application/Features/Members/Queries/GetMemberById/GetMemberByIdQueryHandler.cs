using LibraryManagement.Application.Exceptions;
using LibraryManagement.Application.Features.Members.Responses;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Queries.GetMemberById;

public sealed class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberResponse>
{
    private readonly IMemberRepository _memberRepository;

    public GetMemberByIdQueryHandler(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<MemberResponse> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.Id, cancellationToken);

        if (member is null)
            throw new NotFoundException(nameof(Member), request.Id);

        return new MemberResponse(
            member.Id,
            member.FullName.FirstName,
            member.FullName.LastName,
            member.Email.Value,
            member.Status);
    }
}