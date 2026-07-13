using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.CreateAuthor;

public sealed record CreateAuthorCommand(
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    string? Biography) : IRequest<Guid>;