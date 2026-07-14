using MediatR;

namespace LibraryManagement.Application.Features.Categories.Commands.CreateCategory;

public sealed record CreateCategoryCommand(
    string Name) : IRequest<Guid>;