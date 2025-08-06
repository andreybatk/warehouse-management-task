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
    /// Создать документ поступления
    /// </summary>
    /// <param name="command">Тело запроса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор созданного документа поступления</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateReceiptDocumentCommand command, CancellationToken token)
    {
        var id = await mediator.Send(command, token);

        return Ok(id);
    }

    /// <summary>
    /// Получить документ поступления
    /// </summary>
    /// <param name="id">Идентификатор документа поступления</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор обновленного документа поступления</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ReceiptDocumentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken token)
    {
        var document = await mediator.Send(new GetDocumentQuery(id), token);

        return Ok(document);
    }

    /// <summary>
    /// Получить все номера документов поступления
    /// </summary>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Коллекцию номеров документов поступления</returns>
    [HttpGet("numbers")]
    [ProducesResponseType(typeof(ReceiptDocumentResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNumbers(CancellationToken token)
    {
        var numbers = await mediator.Send(new GetNumbersDocumentsQuery(), token);

        return Ok(numbers);
    }

    /// <summary>
    /// Получить документы поступления с ресурсами и фильтрацией
    /// </summary>
    /// <param name="command">Тело запроса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Коллекция документов поступления</returns>
    [HttpPost("search")]
    [ProducesResponseType(typeof(List<ReceiptDocumentResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromBody] GetDocumentsQuery command, CancellationToken token)
    {
        var documents = await mediator.Send(command, token);

        return Ok(documents);
    }

    /// <summary>
    /// Удалить документ поступления
    /// </summary>
    /// <param name="id">Идентификатор документа поступления</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор удаленного документа поступления</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(Guid?), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        var deletedId = await mediator.Send(new DeleteReceiptDocumentCommand(id), token);

        return Ok(deletedId);
    }

    //TODO: Сделать обновление всего документа и ресурсов одним запросом
    /// <summary>
    /// Обновить документ поступления
    /// </summary>
    /// <param name="id">Идентификатор документа поступления</param>
    /// <param name="request">Тело запроса</param>
    /// <param name="token">Cancellation Token</param>
    /// <returns>Идентификатор обновленного документа поступления</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(Guid?), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateReceiptDocumentRequest request, CancellationToken token)
    {
        var updatedId = await mediator.Send(new UpdateReceiptDocumentCommand(id, request.Number, request.CreatedAt), token);

        return Ok(updatedId);
    }
}