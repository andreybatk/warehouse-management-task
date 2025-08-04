using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Units.DTOs;

/// <summary>
/// Единица измерения
/// </summary>
/// <param name="Id">Идентификатор единицы измерения</param>
/// <param name="Name">Наименование</param>
/// <param name="State">Состояние</param>
public record UnitResponse(Guid Id, string Name, EState State);