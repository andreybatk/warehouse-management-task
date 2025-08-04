using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Units.Commands;

/// <summary>
/// Команда на изменение состояние единицы измерения
/// </summary>
/// <param name="Id">Идентификатор единицы измерения</param>
/// <param name="NewState">Новое состояние</param>
public record ChangeUnitStateCommand(Guid Id, EState NewState) : ICommand<Guid>;
