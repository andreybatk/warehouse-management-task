using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Units.Commands;

/// <summary>
/// Команда на создание единицы измерения
/// </summary>
/// <param name="Name">Наименование</param>
/// <param name="State">Состояние</param>
public sealed record CreateUnitCommand(
    string Name,
    EState State
) : ICommand<Guid>;
