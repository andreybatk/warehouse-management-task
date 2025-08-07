namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;

/// <summary>
/// Запрос на обновление ресурсов поступления в документе поступления
/// </summary>
public class UpdateReceiptDocumentResourceRequest
{
    /// <summary>
    /// Идентификатор ресурса поступления (null - если нужно создать новый)
    /// </summary>
    public Guid? Id { get; set; }
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