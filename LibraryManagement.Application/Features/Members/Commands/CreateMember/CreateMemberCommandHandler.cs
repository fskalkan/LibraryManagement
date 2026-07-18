using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Domain.ValueObjects;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.CreateMember;

public sealed class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, Guid>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);

        var exists = await _memberRepository.ExistsByEmailAsync(email, cancellationToken);

        if (exists)
            throw new ConflictException("Member with this email already exists.");

        var fullName = FullName.Create(request.FirstName, request.LastName);

        var member = Member.Create(fullName, email);

        await _memberRepository.AddAsync(member, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return member.Id;
    }
}