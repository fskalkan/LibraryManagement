namespace LibraryManagement.Api.Contracts.Authors;

public sealed record UpdateAuthorRequest(
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    string? Biography);