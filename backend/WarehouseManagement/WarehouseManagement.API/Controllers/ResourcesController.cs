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
    /// <param name="token">Cancellation Token</param>
    /// <returns>Коллекция ресурсов</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ResourceResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] EState? state, CancellationToken token)
    {
        var resources = await mediator.Send(new GetResourcesQuery(state), token);

        return Ok(resources);
    }

    /// <summary>
    /// Получить ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Коллекция ресурсов</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ResourceResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid id, CancellationToken token)
    {
        var resource = await mediator.Send(new GetResourceQuery(id), token);

        return Ok(resource);
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
        var command = new UpdateResourceCommand(id, request.Name);

        var updatedResourceId = await mediator.Send(command, token);

        return Ok(updatedResourceId);
    }

    /// <summary>
    /// Архивировать ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <param name="token">Cancellation Token</param>    /// <returns>Идентификатор архивированного ресурса</returns>
    [HttpPatch("{id:guid}/archive")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Archive(Guid id, CancellationToken token)
    {
        var archivedId = await mediator.Send(new ChangeResourceStateCommand(id, EState.Archived), token);

        return Ok(archivedId);
    }

    /// <summary>
    /// Активировать ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор активированного ресурса</returns>
    [HttpPatch("{id:guid}/activate")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate(Guid id, CancellationToken token)
    {
        var activatedId = await mediator.Send(new ChangeResourceStateCommand(id, EState.Active), token);

        return Ok(activatedId);
    }

    /// <summary>
    /// Удалить ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор удаленного ресурса</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Guid?), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        var deletedId = await mediator.Send(new DeleteResourceCommand(id), token);

        return Ok(deletedId);
    }
}
