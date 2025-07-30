using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.BusinessLogic.Resources.Commands;
using WarehouseManagement.BusinessLogic.Resources.DTOs;
using WarehouseManagement.BusinessLogic.Resources.Queries;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ResourcesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ResourcesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ResourceResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] State? state)
    {
        var resources = await _mediator.Send(new GetResourcesQuery(state));
        return Ok(resources);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAccount([FromBody] CreateResourceRequest request, CancellationToken token)
    {
        var command = new CreateResourceCommand(request.Name, request.State);

        var resourceId = await _mediator.Send(command, token);

        return Ok(resourceId);
    }

    [HttpPut("{resourceId:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAccount(Guid resourceId, [FromBody] UpdateResourceRequest request, CancellationToken token)
    {
        var command = new UpdateResourceCommand(
            resourceId,
            request.Name);

        var updatedResourceId = await _mediator.Send(command, token);

        return Ok(updatedResourceId);
    }

    [HttpPatch("{resourceId:guid}/archive")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Archive(Guid resourceId)
    {
        var archivedId = await _mediator.Send(new ChangeResourceStateCommand(resourceId, State.Archived));

        return Ok(archivedId);
    }

    [HttpPatch("{resourceId:guid}/activate")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate(Guid resourceId)
    {
        var activatedId = await _mediator.Send(new ChangeResourceStateCommand(resourceId, State.Active));

        return Ok(activatedId);
    }
}
