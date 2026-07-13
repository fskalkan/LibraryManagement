namespace LibraryManagement.Api.Models;

public sealed record ValidationError(
    string Field,
    string Message);