using FluentValidation;

namespace LibraryManagement.Application.Features.Books.Queries.GetBookById;

public sealed class GetBookByIdQueryValidator
    : AbstractValidator<GetBookByIdQuery>
{
    public GetBookByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Book id is required.");
    }
}