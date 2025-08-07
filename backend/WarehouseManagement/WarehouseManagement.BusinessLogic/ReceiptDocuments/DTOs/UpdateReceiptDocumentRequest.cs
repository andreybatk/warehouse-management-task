namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;

/// <summary>
/// Запрос на обновление документа поступления
/// </summary>
public class UpdateReceiptDocumentRequest
{
    /// <summary>
    /// Номер документа
    /// </summary>
    public long Number { get; set; }
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Ресурсы документа поступления
    /// </summary>
    public List<UpdateReceiptDocumentResourceRequest> ReceiptResources { get; set; } = [];
}