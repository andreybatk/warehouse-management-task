using WarehouseManagement.BusinessLogic.Abstractions.Messaging;

namespace WarehouseManagement.BusinessLogic.ReceiptResources.Commands;

/// <summary>
/// Команда на удаление ресурса поступления
/// </summary>
/// <param name="Id">Идентификатор ресурса поступления</param>
public sealed record DeleteReceiptResourceCommand(Guid Id) : ICommand<Guid>;
