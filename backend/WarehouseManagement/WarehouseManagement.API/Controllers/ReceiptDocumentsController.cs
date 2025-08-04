using MediatR;
using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.Commands;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.Queries;

namespace WarehouseManagement.API.Controllers;

/// <summary>
/// Документы поступления
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ReceiptDocumentsController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Создание документа поступления
    /// </summary>
    /// <param name="command">Тело запроса</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReceiptDocument([FromBody] CreateReceiptDocumentCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(id);
    }

    /// <summary>
    /// Получить документ поступления
    /// </summary>
    /// <param name="id">Идентификатор документа поступления</param>
    /// <returns>Идентификатор обновленного документа поступления</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReceiptDocumentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id)
    {
        var document = await mediator.Send(new GetDocumentQuery(id));

        return Ok(document);
    }

    /// <summary>
    /// Получить документы поступления с ресурсами и фильтрацией
    /// </summary>
    /// <param name="command">Тело запроса</param>
    /// <returns></returns>
    [HttpPost("search")]
    [ProducesResponseType(typeof(List<ReceiptDocumentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromBody] GetDocumentsQuery command)
    {
        var documents = await mediator.Send(command);

        return Ok(documents);
    }

    /// <summary>
    /// Удалить документ поступления
    /// </summary>
    /// <param name="id">Идентификатор документа поступления</param>
    /// <returns>Идентификатор удаленного документа поступления</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Guid?), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletedId = await mediator.Send(new DeleteReceiptDocumentCommand(id));

        return Ok(deletedId);
    }

    /// <summary>
    /// Обновить документ поступления
    /// </summary>
    /// <param name="id">Идентификатор документа поступления</param>
    /// <param name="request">Тело запроса</param>
    /// <returns>Идентификатор обновленного документа поступления</returns>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(Guid?), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReceiptDocumentRequest request)
    {
        var updatedId = await mediator.Send(new UpdateReceiptDocumentCommand(id, request.Number, request.CreatedAt));

        return Ok(updatedId);
    }
}