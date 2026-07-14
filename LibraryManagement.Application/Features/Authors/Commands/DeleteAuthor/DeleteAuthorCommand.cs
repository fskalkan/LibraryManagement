using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.DeleteAuthor;

public sealed record DeleteAuthorCommand(Guid Id) : IRequest;