namespace LibraryManagement.Api.Models;

public sealed record ErrorResponse(
    int StatusCode,
    string Message,
    IEnumerable<ValidationError>? Errors = null);