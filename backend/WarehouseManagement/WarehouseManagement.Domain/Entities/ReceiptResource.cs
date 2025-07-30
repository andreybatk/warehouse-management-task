namespace WarehouseManagement.Domain.Entities;

public class ReceiptResource
{
    /// <summary>
    /// Идентификатор ресурса поступления
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Идентификатор документа поступления
    /// </summary>
    public Guid ReceiptDocumentId { get; set; }
    /// <summary>
    /// Документ поступления
    /// </summary>
    public ReceiptDocument ReceiptDocument { get; set; } = null!;
    /// <summary>
    /// Идентификатор ресурса
    /// </summary>
    public Guid ResourceId { get; set; }
    /// <summary>
    /// Ресурс
    /// </summary>
    public Resource Resource { get; set; } = null!;
    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    public Guid UnitId { get; set; }
    /// <summary>
    /// Единица измерения
    /// </summary>
    public Unit Unit { get; set; } = null!;
    /// <summary>
    /// Количество
    /// </summary>
    public long Quantity { get; set; }
}