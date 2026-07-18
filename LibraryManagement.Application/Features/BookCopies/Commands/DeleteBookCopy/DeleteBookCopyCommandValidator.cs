using FluentValidation;

namespace LibraryManagement.Application.Features.BookCopies.Commands.DeleteBookCopy;

public sealed class DeleteBookCopyCommandValidator
    : AbstractValidator<DeleteBookCopyCommand>
{
    public DeleteBookCopyCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Book copy id is required.");
    }
}