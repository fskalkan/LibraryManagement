using LibraryManagement.Api.Contracts.Members;
using LibraryManagement.Application.Features.Members.Commands.CreateMember;
using LibraryManagement.Application.Features.Members.Commands.DeleteMember;
using LibraryManagement.Application.Features.Members.Commands.UpdateMember;
using LibraryManagement.Application.Features.Members.Queries.GetMemberById;
using LibraryManagement.Application.Features.Members.Queries.GetMembers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers;

[Route("api/members")]
[ApiController]
public sealed class MembersController : ControllerBase
{
    private readonly IMediator _mediator;

    public MembersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var members = await _mediator.Send(new GetMembersQuery(), cancellationToken);

        return Ok(members);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var member = await _mediator.Send(new GetMemberByIdQuery(id), cancellationToken);

        return Ok(member);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberCommand command, CancellationToken cancellationToken)
    {
        var id = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id },
            id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateMemberRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateMemberCommand(
            id,
            request.FirstName,
            request.LastName,
            request.Email);

        await _mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteMemberCommand(id), cancellationToken);

        return NoContent();
    }
}