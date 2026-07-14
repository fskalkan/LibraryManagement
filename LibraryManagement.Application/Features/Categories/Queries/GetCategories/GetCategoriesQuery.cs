using LibraryManagement.Application.Features.Categories.Responses;
using MediatR;

namespace LibraryManagement.Application.Features.Categories.Queries.GetCategories;

public sealed record GetCategoriesQuery
    : IRequest<IReadOnlyList<CategoryResponse>>;