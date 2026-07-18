using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using LibraryManagement.Domain.ValueObjects;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.UpdateMember;

public sealed class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMemberCommandHandler(
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.Id, cancellationToken);

        if (member is null)
            throw new NotFoundException(nameof(Member), request.Id);

        var email = Email.Create(request.Email);

        var exists = await _memberRepository.ExistsByEmailExceptMemberAsync(request.Id, email, cancellationToken);

        if (exists)
            throw new ConflictException($"Member with email '{request.Email}' already exists.");

        var fullName = FullName.Create(request.FirstName, request.LastName);

        member.ChangeName(fullName);
        member.ChangeEmail(email);

        _memberRepository.Update(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}