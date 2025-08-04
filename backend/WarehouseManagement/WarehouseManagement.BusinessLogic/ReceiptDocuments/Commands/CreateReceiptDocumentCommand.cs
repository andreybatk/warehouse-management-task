using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Commands;

/// <summary>
/// Команда на создание документа поступления
/// </summary>
/// <param name="Number">Номер документа</param>
/// <param name="CreatedAt">Дата создания</param>
/// <param name="ReceiptResources">Ресурсы поступления</param>
public sealed record CreateReceiptDocumentCommand(
    long Number,
    DateTime CreatedAt,
    List<CreateReceiptRequest> ReceiptResources
) : ICommand<Guid>;
