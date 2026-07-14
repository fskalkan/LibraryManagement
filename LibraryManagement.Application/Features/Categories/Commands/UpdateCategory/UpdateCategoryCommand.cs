using MediatR;

namespace LibraryManagement.Application.Features.Categories.Commands.UpdateCategory;

public sealed record UpdateCategoryCommand(
    Guid Id,
    string Name) : IRequest;