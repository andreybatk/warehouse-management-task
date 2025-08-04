using WarehouseManagement.BusinessLogic.Abstractions.Messaging;

namespace WarehouseManagement.BusinessLogic.Units.Commands;

/// <summary>
/// Команда на обновление единицы измерения
/// </summary>
/// <param name="Id">Идентификатор единицы измерения</param>
/// <param name="Name">Наименование</param>
public sealed record UpdateUnitCommand(
    Guid Id,
    string Name
) : ICommand<Guid>;