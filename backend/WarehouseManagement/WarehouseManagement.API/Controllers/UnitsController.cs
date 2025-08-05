using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.BusinessLogic.Units.Commands;
using WarehouseManagement.BusinessLogic.Units.DTOs;
using WarehouseManagement.BusinessLogic.Units.Queries;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.API.Controllers;

/// <summary>
/// Единицы измерения
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class UnitsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Получить все единицы измерения
    /// </summary>
    /// <param name="state">Состояние</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Коллекция единиц измерения</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<UnitResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] EState? state, CancellationToken token)
    {
        var units = await mediator.Send(new GetUnitsQuery(state), token);

        return Ok(units);
    }

    /// <summary>
    /// Получить единицу измерения
    /// </summary>
    /// <param name="id">Идентификатор единицы измерения</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Коллекция единиц измерения</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(UnitResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(Guid id, CancellationToken token)
    {
        var unit = await mediator.Send(new GetUnitQuery(id), token);

        return Ok(unit);
    }

    /// <summary>
    /// Создать единицу измерения
    /// </summary>
    /// <param name="command">Тело запроса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор созданной единицы измерения</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUnitCommand command, CancellationToken token)
    {
        var unitId = await mediator.Send(command, token);

        return Ok(unitId);
    }

    /// <summary>
    /// Обновить единицу измерения
    /// </summary>
    /// <param name="id">Идентификатор единицы измерения</param>
    /// <param name="request">Тело запроса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор обновленной единицы измерения</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUnitRequest request, CancellationToken token)
    {
        var command = new UpdateUnitCommand(id,  request.Name);

        var updatedUnitId = await mediator.Send(command, token);

        return Ok(updatedUnitId);
    }

    /// <summary>
    /// Архивировать единицу измерения
    /// </summary>
    /// <param name="id">Идентификатор единицы измерения</param>
    /// <param name="token">Cancellation Token</param> 
    /// <returns>Идентификатор архивированной единицы измерения</returns>
    [HttpPatch("{id:guid}/archive")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Archive(Guid id, CancellationToken token)
    {
        var archivedId = await mediator.Send(new ChangeUnitStateCommand(id, EState.Archived), token);

        return Ok(archivedId);
    }

    /// <summary>
    /// Активировать единицу измерения
    /// </summary>
    /// <param name="id">Идентификатор единицы измерения</param>
    /// <param name="token">Cancellation Token</param> 
    /// <returns>Идентификатор активированной единицы измерения</returns>
    [HttpPatch("{id:guid}/activate")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate(Guid id, CancellationToken token)
    {
        var activatedId = await mediator.Send(new ChangeUnitStateCommand(id, EState.Active), token);

        return Ok(activatedId);
    }

    /// <summary>
    /// Удалить единицу измерения
    /// </summary>
    /// <param name="id">Идентификатор единицы измерения</param>
    /// <param name="token">Cancellation Token</param> 
    /// <returns>Идентификатор удаленной единицы измерения</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Guid?), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        var deletedId = await mediator.Send(new DeleteUnitCommand(id), token);

        return Ok(deletedId);
    }
}