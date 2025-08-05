using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Units.DTOs;

namespace WarehouseManagement.BusinessLogic.Units.Queries;

/// <summary>
/// Запрос на получение единицы измерения
/// </summary>
/// <param name="Id">Идентификатор единицы измерения</param>
public sealed record GetUnitQuery(Guid Id) : IQuery<UnitResponse>;
