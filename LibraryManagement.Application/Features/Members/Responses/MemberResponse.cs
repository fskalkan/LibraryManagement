using LibraryManagement.Domain.Enums;

namespace LibraryManagement.Application.Features.Members.Responses;

public sealed record MemberResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    MemberStatus Status);