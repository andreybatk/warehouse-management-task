using WarehouseManagement.BusinessLogic.Abstractions.Messaging;

namespace WarehouseManagement.BusinessLogic.Resources.Commands;

/// <summary>
/// Команда на обновление ресурса
/// </summary>
/// <param name="Id">Идентификатор ресурса</param>
/// <param name="Name">Наименование</param>
public sealed record UpdateResourceCommand(
    Guid Id,
    string Name
) : ICommand<Guid>;
