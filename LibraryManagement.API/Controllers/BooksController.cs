using LibraryManagement.Api.Contracts.Books;
using LibraryManagement.Application.Features.Books.Commands.CreateBook;
using LibraryManagement.Application.Features.Books.Commands.DeleteBook;
using LibraryManagement.Application.Features.Books.Commands.UpdateBook;
using LibraryManagement.Application.Features.Books.Queries.GetBookById;
using LibraryManagement.Application.Features.Books.Queries.GetBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[ApiController]
[Route("api/books")]
public sealed class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBooksQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBookByIdQuery(id), cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateBookRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateBookCommand(
            id,
            request.Title,
            request.Isbn,
            request.PublishYear,
            request.AuthorId,
            request.CategoryId);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBookCommand(id), cancellationToken);

        return NoContent();
    }
}