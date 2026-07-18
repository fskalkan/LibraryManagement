using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Entities;
using LibraryManagement.Domain.Repositories;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.DeleteMember;

public sealed class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMemberCommandHandler(
        IMemberRepository memberRepository,
        IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(request.Id, cancellationToken);

        if (member is null)
            throw new NotFoundException(nameof(Member), request.Id);

        _memberRepository.Remove(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}