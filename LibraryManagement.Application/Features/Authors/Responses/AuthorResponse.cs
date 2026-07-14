namespace LibraryManagement.Application.Features.Authors.Responses;

public sealed record AuthorResponse(
    Guid Id,
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    string? Biography);