using FluentValidation;

namespace LibraryManagement.Application.Features.Books.Commands.UpdateBook;

public sealed class UpdateBookCommandValidator
    : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Book id is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Isbn)
            .NotEmpty()
            .Length(10, 13);

        RuleFor(x => x.PublishYear)
            .LessThanOrEqualTo(DateTime.UtcNow.Year)
            .WithMessage("Publish year cannot be in the future.");

        RuleFor(x => x.AuthorId)
            .NotEmpty()
            .WithMessage("Author id is required.");

        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage("Category id is required.");
    }
}