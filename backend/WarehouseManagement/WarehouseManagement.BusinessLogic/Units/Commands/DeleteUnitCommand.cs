using WarehouseManagement.BusinessLogic.Abstractions.Messaging;

namespace WarehouseManagement.BusinessLogic.Units.Commands;

/// <summary>
/// Команда на удаление единицы измерения
/// </summary>
/// <param name="Id">Идентификатор единицы измерения</param>
public record DeleteUnitCommand(Guid Id) : ICommand<Guid?>;