using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.UpdateAuthor;

public sealed record UpdateAuthorCommand(
    Guid Id,
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    string? Biography) : IRequest;