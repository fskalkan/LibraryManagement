using FluentValidation;

namespace LibraryManagement.Application.Features.BookCopies.Commands.UpdateBookCopy;

public sealed class UpdateBookCopyCommandValidator
    : AbstractValidator<UpdateBookCopyCommand>
{
    public UpdateBookCopyCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Book copy id is required.");

        RuleFor(x => x.Barcode)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ShelfLocation)
            .NotEmpty()
            .MaximumLength(100);
    }
}