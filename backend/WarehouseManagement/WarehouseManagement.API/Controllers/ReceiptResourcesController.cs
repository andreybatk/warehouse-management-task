using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.BusinessLogic.ReceiptResources.Commands;
using WarehouseManagement.BusinessLogic.ReceiptResources.DTOs;

namespace WarehouseManagement.API.Controllers;

/// <summary>
/// Ресурсы поступления
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReceiptResourcesController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Создать ресурс поступления
    /// </summary>
    /// <param name="command">Тело запроса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор созданного ресурса поступления</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] AddReceiptResourceCommand command, CancellationToken token)
    {
        var id = await mediator.Send(command, token);

        return Ok(id);
    }

    /// <summary>
    /// Обновить ресурс поступления
    /// </summary>
    /// <param name="id">Идентификатор ресурса поступления</param>
    /// <param name="request">Тело запроса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор обновленного ресурса поступления</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReceiptResourceRequest request, CancellationToken token)
    {
        var command = new UpdateReceiptResourceCommand(id, request.ReceiptDocumentId, request.ResourceId,
            request.UnitId, request.Quantity);

        var updatedId = await mediator.Send(command, token);

        return Ok(updatedId);
    }

    /// <summary>
    /// Удалить ресурс поступления
    /// </summary>
    /// <param name="id">Идентификатор ресурса поступления</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор удаленного ресурса поступления</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        var deletedId = await mediator.Send(new DeleteReceiptResourceCommand(id), token);

        return Ok(deletedId);
    }
}

