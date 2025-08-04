using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Resources.Commands;

/// <summary>
/// Команда на создание ресурса
/// </summary>
/// <param name="Name">Наименование</param>
/// <param name="State">Состояние</param>
public sealed record CreateResourceCommand(
    string Name,
    EState State
) : ICommand<Guid>;
