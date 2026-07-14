using FluentValidation;

namespace LibraryManagement.Application.Features.Authors.Commands.UpdateAuthor;

public sealed class UpdateAuthorCommandValidator
    : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Author id is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.BirthDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Birth date cannot be in the future.");

        RuleFor(x => x.Biography)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.Biography));
    }
}