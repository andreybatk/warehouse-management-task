using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Commands;

/// <summary>
/// Команда на обновление документа поступления
/// </summary>
/// <param name="Id">Идентификатор документа поступления</param>
/// <param name="Number">Номер документа поступления</param>
/// <param name="CreatedAt">Дата создания документа поступления</param>
public sealed record UpdateReceiptDocumentCommand(
    Guid Id,
    long Number,
    DateTime CreatedAt
) : ICommand<Guid>;
