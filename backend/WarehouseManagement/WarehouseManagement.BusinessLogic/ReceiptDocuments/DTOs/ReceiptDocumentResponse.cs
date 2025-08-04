namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;

/// <summary>
/// Документ поступления
/// </summary>
public class ReceiptDocumentResponse
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
    /// Дата создания документа поступления
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Ресурсы поступления
    /// </summary>
    public List<ReceiptResourceResponse> ReceiptResources { get; set; } = [];
}
