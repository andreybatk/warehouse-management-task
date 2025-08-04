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
    /// <returns>Коллекция единиц измерения</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<UnitResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] EState? state)
    {
        var units = await mediator.Send(new GetUnitsQuery(state));
        return Ok(units);
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
    public async Task<IActionResult> CreateUnit([FromBody] CreateUnitCommand command, CancellationToken token)
    {
        var unitId = await mediator.Send(command, token);

        return Ok(unitId);
    }

    /// <summary>
    /// Обновить единицу измерения
    /// </summary>
    /// <param name="unitId">Идентификатор единицы измерения</param>
    /// <param name="request">Тело запроса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор обновленной единицы измерения</returns>
    [HttpPut("{unitId:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUnit(Guid unitId, [FromBody] UpdateUnitRequest request, CancellationToken token)
    {
        var command = new UpdateUnitCommand(
            unitId,
            request.Name);

        var updatedUnitId = await mediator.Send(command, token);

        return Ok(updatedUnitId);
    }

    /// <summary>
    /// Архивировать единицу измерения
    /// </summary>
    /// <param name="unitId">Идентификатор единицы измерения</param>
    /// <returns>Идентификатор архивированной единицы измерения</returns>
    [HttpPatch("{unitId:guid}/archive")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Archive(Guid unitId)
    {
        var archivedId = await mediator.Send(new ChangeUnitStateCommand(unitId, EState.Archived));

        return Ok(archivedId);
    }

    /// <summary>
    /// Активировать единицу измерения
    /// </summary>
    /// <param name="unitId">Идентификатор единицы измерения</param>
    /// <returns>Идентификатор активированной единицы измерения</returns>
    [HttpPatch("{unitId:guid}/activate")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate(Guid unitId)
    {
        var activatedId = await mediator.Send(new ChangeUnitStateCommand(unitId, EState.Active));

        return Ok(activatedId);
    }

    /// <summary>
    /// Удалить единицу измерения
    /// </summary>
    /// <param name="unitId">Идентификатор единицы измерения</param>
    /// <returns>Идентификатор удаленной единицы измерения</returns>
    [HttpDelete("{unitId:guid}")]
    [ProducesResponseType(typeof(Guid?), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid unitId)
    {
        var deletedId = await mediator.Send(new DeleteUnitCommand(unitId));

        return Ok(deletedId);
    }
}
