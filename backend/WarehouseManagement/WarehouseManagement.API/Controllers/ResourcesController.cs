using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.BusinessLogic.Resources.Commands;
using WarehouseManagement.BusinessLogic.Resources.DTOs;
using WarehouseManagement.BusinessLogic.Resources.Queries;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.API.Controllers;

/// <summary>
/// Ресурсы
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ResourcesController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Получить все ресурсы
    /// </summary>
    /// <param name="state">Состояние</param>
    /// <returns>Коллекция ресурсов</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ResourceResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] EState? state)
    {
        var resources = await mediator.Send(new GetResourcesQuery(state));
        return Ok(resources);
    }

    /// <summary>
    /// Создать ресурс
    /// </summary>
    /// <param name="command">Тело запроса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор созданного ресурса</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateResourceCommand command, CancellationToken token)
    {
        var resourceId = await mediator.Send(command, token);

        return Ok(resourceId);
    }

    /// <summary>
    /// Обновить ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <param name="request">Тело запроса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор обновленного ресурса</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateResourceRequest request, CancellationToken token)
    {
        var command = new UpdateResourceCommand(
            id,
            request.Name);

        var updatedResourceId = await mediator.Send(command, token);

        return Ok(updatedResourceId);
    }

    /// <summary>
    /// Архивировать ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <returns>Идентификатор архивированного ресурса</returns>
    [HttpPatch("{id:guid}/archive")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Archive(Guid id)
    {
        var archivedId = await mediator.Send(new ChangeResourceStateCommand(id, EState.Archived));

        return Ok(archivedId);
    }

    /// <summary>
    /// Активировать ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <returns>Идентификатор активированного ресурса</returns>
    [HttpPatch("{id:guid}/activate")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate(Guid id)
    {
        var activatedId = await mediator.Send(new ChangeResourceStateCommand(id, EState.Active));

        return Ok(activatedId);
    }

    /// <summary>
    /// Удалить ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <returns>Идентификатор удаленного ресурса</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Guid?), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedId = await mediator.Send(new DeleteResourceCommand(id));

        return Ok(deletedId);
    }
}
