using FluentValidation;
using LibraryManagement.Api.Models;
using LibraryManagement.Application.Exceptions;
using LibraryManagement.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace LibraryManagement.Api.ExceptionHandlers;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, message, errors) = exception switch
        {
            ValidationException ex => (
                StatusCodes.Status400BadRequest,
                "Validation failed.",
                ex.Errors.Select(error => new ValidationError(
                    Field: error.PropertyName,
                    Message: error.ErrorMessage))
            ),

            DomainException ex => (
                StatusCodes.Status400BadRequest,
                ex.Message,
                null
            ),

            NotFoundException ex => (
                StatusCodes.Status404NotFound,
                ex.Message,
                null
            ),

            ConflictException ex => (
                StatusCodes.Status409Conflict,
                ex.Message,
                null
            ),

            _ => (
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.",
                null
            )
        };

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            new ErrorResponse(
                StatusCode: statusCode,
                Message: message,
                Errors: errors),
            cancellationToken);

        return true;
    }
}