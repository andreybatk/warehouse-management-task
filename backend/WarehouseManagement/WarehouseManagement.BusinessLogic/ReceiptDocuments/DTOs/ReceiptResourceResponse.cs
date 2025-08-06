namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;

/// <summary>
/// Ресурс поступления
/// </summary>
public class ReceiptResourceResponse
{
    /// <summary>
    /// Идентификатор ресурса поступления
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public Guid ResourceId { get; set; }
    /// <summary>
    /// Наименование ресурса
    /// </summary>
    public string ResourceName { get; set; } = string.Empty;
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public Guid UnitId { get; set; }
    /// <summary>
    /// Наименование единицы измерения
    /// </summary>
    public string UnitName { get; set; } = string.Empty;
    /// <summary>
    /// Количество
    /// </summary>
    public long Quantity { get; set; }
}
