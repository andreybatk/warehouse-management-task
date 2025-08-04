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
    public async Task<IActionResult> CreateResource([FromBody] CreateResourceCommand command, CancellationToken token)
    {
        var resourceId = await mediator.Send(command, token);

        return Ok(resourceId);
    }

    /// <summary>
    /// Обновить ресурс
    /// </summary>
    /// <param name="resourceId">Идентификатор ресурса</param>
    /// <param name="request">Тело запроса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор обновленного ресурса</returns>
    [HttpPut("{resourceId:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateResource(Guid resourceId, [FromBody] UpdateResourceRequest request, CancellationToken token)
    {
        var command = new UpdateResourceCommand(
            resourceId,
            request.Name);

        var updatedResourceId = await mediator.Send(command, token);

        return Ok(updatedResourceId);
    }

    /// <summary>
    /// Архивировать ресурс
    /// </summary>
    /// <param name="resourceId">Идентификатор ресурса</param>
    /// <returns>Идентификатор архивированного ресурса</returns>
    [HttpPatch("{resourceId:guid}/archive")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Archive(Guid resourceId)
    {
        var archivedId = await mediator.Send(new ChangeResourceStateCommand(resourceId, EState.Archived));

        return Ok(archivedId);
    }

    /// <summary>
    /// Активировать ресурс
    /// </summary>
    /// <param name="resourceId">Идентификатор ресурса</param>
    /// <returns>Идентификатор активированного ресурса</returns>
    [HttpPatch("{resourceId:guid}/activate")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate(Guid resourceId)
    {
        var activatedId = await mediator.Send(new ChangeResourceStateCommand(resourceId, EState.Active));

        return Ok(activatedId);
    }

    /// <summary>
    /// Удалить ресурс
    /// </summary>
    /// <param name="resourceId">Идентификатор ресурса</param>
    /// <returns>Идентификатор удаленного ресурса</returns>
    [HttpDelete("{resourceId:guid}")]
    [ProducesResponseType(typeof(Guid?), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid resourceId)
    {
        var deletedId = await mediator.Send(new DeleteResourceCommand(resourceId));

        return Ok(deletedId);
    }
}
