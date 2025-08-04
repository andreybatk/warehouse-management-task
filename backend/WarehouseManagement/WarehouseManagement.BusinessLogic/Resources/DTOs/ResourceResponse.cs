using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Resources.DTOs;

/// <summary>
/// Ресурс
/// </summary>
/// <param name="Id">Идентификатор ресурса</param>
/// <param name="Name">Наименование</param>
/// <param name="State">Состояние</param>
public record ResourceResponse(Guid Id, string Name, EState State);