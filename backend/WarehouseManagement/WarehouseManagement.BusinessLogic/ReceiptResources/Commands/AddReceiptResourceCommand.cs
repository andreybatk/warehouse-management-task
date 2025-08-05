using WarehouseManagement.BusinessLogic.Abstractions.Messaging;

namespace WarehouseManagement.BusinessLogic.ReceiptResources.Commands;

/// <summary>
/// Команда на создание ресурса поступления
/// </summary>
/// <param name="ReceiptDocumentId">Идентификатор документа поступления</param>
/// <param name="ResourceId">Идентификатор ресурса</param>
/// <param name="UnitId">Идентификатор единицы измерения</param>
/// <param name="Quantity">Количество</param>
public sealed record AddReceiptResourceCommand(
    Guid ReceiptDocumentId,
    Guid ResourceId,
    Guid UnitId,
    long Quantity
) : ICommand<Guid>;
