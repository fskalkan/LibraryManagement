using FluentValidation;

namespace LibraryManagement.Application.Features.Members.Queries.GetMemberById;

public sealed class GetMemberByIdQueryValidator
    : AbstractValidator<GetMemberByIdQuery>
{
    public GetMemberByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}