using LibraryManagement.Api.Contracts.BookCopies;
using LibraryManagement.Application.Features.BookCopies.Commands.CreateBookCopy;
using LibraryManagement.Application.Features.BookCopies.Commands.DeleteBookCopy;
using LibraryManagement.Application.Features.BookCopies.Commands.UpdateBookCopy;
using LibraryManagement.Application.Features.BookCopies.Queries.GetBookCopies;
using LibraryManagement.Application.Features.BookCopies.Queries.GetBookCopyById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/bookcopies")]
public sealed class BookCopiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookCopiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBookCopiesQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBookCopyByIdQuery(id), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookCopyCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateBookCopyRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateBookCopyCommand(
            id,
            request.Barcode,
            request.ShelfLocation);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBookCopyCommand(id), cancellationToken);

        return NoContent();
    }
}