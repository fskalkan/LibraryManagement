using FluentValidation;

namespace LibraryManagement.Application.Features.BookCopies.Queries.GetBookCopyById;

public sealed class GetBookCopyByIdQueryValidator
    : AbstractValidator<GetBookCopyByIdQuery>
{
    public GetBookCopyByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Book copy id is required.");
    }
}