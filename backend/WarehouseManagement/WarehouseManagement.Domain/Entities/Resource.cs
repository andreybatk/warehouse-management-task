using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Domain.Entities;

public class Resource
{
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Наименование ресурса
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Состояние ресурса
    /// </summary>
    public State State { get; set; }
}
