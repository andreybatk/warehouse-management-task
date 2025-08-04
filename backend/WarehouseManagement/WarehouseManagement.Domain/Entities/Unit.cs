using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Domain.Entities;

public class Unit
{
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Наименование единицы измерения
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Состояние единицы измерения
    /// </summary>
    public EState State { get; set; }
}
