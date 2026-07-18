using FluentValidation;

public sealed class CreateBookCopyCommandValidator
    : AbstractValidator<CreateBookCopyCommand>
{
    public CreateBookCopyCommandValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty();

        RuleFor(x => x.Barcode)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ShelfLocation)
            .NotEmpty()
            .MaximumLength(100);
    }
}