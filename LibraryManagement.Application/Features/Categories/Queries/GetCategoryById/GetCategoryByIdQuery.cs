using LibraryManagement.Application.Features.Categories.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.Categories.Queries.GetCategoryById;

public sealed record GetCategoryByIdQuery(Guid Id)
    : IRequest<CategoryResponse>;