using LibraryManagement.Application.Features.Loans.Commands.CreateLoan;
using LibraryManagement.Application.Features.Loans.Commands.ReturnLoan;
using LibraryManagement.Application.Features.Loans.Queries.GetLoanById;
using LibraryManagement.Application.Features.Loans.Queries.GetLoans;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[Route("api/loans")]
[ApiController]
public sealed class LoansController : ControllerBase
{
    private readonly IMediator _mediator;

    public LoansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var loans = await _mediator.Send(new GetLoansQuery(), cancellationToken);

        return Ok(loans);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var loan = await _mediator.Send(new GetLoanByIdQuery(id), cancellationToken);

        return Ok(loan);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLoanCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            id);
    }

    [HttpPut("{id:guid}/return")]
    public async Task<IActionResult> Return(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ReturnLoanCommand(id), cancellationToken);

        return NoContent();
    }
}