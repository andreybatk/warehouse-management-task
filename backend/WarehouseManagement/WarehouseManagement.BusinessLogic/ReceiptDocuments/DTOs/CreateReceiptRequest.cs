namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;

/// <summary>
/// Ресурс поступления
/// </summary>
public class CreateReceiptRequest
{
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public Guid ResourceId { get; set; }
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public Guid UnitId { get; set; }
    /// <summary>
    /// Количество
    /// </summary>
    public long Quantity { get; set; }
}