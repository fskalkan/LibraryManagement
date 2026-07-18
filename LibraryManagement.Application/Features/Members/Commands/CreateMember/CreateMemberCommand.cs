using MediatR;

public sealed record CreateMemberCommand(
    string FirstName,
    string LastName,
    string Email) : IRequest<Guid>;