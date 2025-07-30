namespace WarehouseManagement.Domain.Entities;

public class ReceiptDocument
{
    /// <summary>
    /// Идентификатор документа поступления
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Номер документа поступления
    /// </summary>
    public long Number { get; set; }
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Ресурсы поступления документа
    /// </summary>
    public List<ReceiptResource> ReceiptResources { get; set; } = [];
}