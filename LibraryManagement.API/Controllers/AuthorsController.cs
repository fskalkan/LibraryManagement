using LibraryManagement.Api.Contracts.Authors;
using LibraryManagement.Application.Features.Authors.Commands.CreateAuthor;
using LibraryManagement.Application.Features.Authors.Commands.DeleteAuthor;
using LibraryManagement.Application.Features.Authors.Commands.UpdateAuthor;
using LibraryManagement.Application.Features.Authors.Queries.GetAuthorById;
using LibraryManagement.Application.Features.Authors.Queries.GetAuthors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAuthorsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetAuthorByIdQuery(id), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command,cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateAuthorRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateAuthorCommand(
            id,
            request.FirstName,
            request.LastName,
            request.BirthDate,
            request.Biography);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteAuthorCommand(id), cancellationToken);

        return NoContent();
    }
}