using FluentValidation;

namespace LibraryManagement.Application.Features.Authors.Queries.GetAuthorById;

public sealed class GetAuthorByIdQueryValidator
    : AbstractValidator<GetAuthorByIdQuery>
{
    public GetAuthorByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Author id is required.");
    }
}