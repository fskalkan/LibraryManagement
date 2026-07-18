using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.DeleteMember;

public sealed record DeleteMemberCommand(Guid Id) : IRequest;