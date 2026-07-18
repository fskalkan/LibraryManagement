using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.UpdateMember;

public sealed record UpdateMemberCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email) : IRequest;