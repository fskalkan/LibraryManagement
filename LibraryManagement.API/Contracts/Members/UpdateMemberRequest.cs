namespace LibraryManagement.Api.Contracts.Members;

public sealed record UpdateMemberRequest(
    string FirstName,
    string LastName,
    string Email);