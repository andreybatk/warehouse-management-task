namespace WarehouseManagement.BusinessLogic.ReceiptResources.DTOs;

public class UpdateReceiptResourceRequest
{
    public Guid ReceiptDocumentId { get; set; }
    public Guid ResourceId { get; set; }
    public Guid UnitId { get; set; }
    public long Quantity { get; set; }
}