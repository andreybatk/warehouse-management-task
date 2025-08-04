using WarehouseManagement.BusinessLogic.Abstractions.Messaging;

namespace WarehouseManagement.BusinessLogic.Resources.Commands;

/// <summary>
/// Команда на удаление ресурса
/// </summary>
/// <param name="Id">Идентификатор ресурса</param>
public record DeleteResourceCommand(Guid Id) : ICommand<Guid?>;
