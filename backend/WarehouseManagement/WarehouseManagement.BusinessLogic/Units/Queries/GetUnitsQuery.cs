using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Units.DTOs;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Units.Queries;

/// <summary>
/// Запрос на получение всех единиц измерения
/// </summary>
/// <param name="State"></param>
public sealed record GetUnitsQuery(EState? State) : IQuery<List<UnitResponse>>;
