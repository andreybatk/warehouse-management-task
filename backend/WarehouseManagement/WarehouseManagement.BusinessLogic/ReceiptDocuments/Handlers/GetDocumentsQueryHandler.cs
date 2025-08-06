using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.Queries;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Handlers;
public class GetDocumentsQueryHandler(IReceiptDocumentRepository receiptDocumentRepository)
    : IQueryHandler<GetDocumentsQuery, List<ReceiptDocumentResponse>>
{
    public async Task<List<ReceiptDocumentResponse>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
    {
        var documents = await receiptDocumentRepository.GetDocumentsAsync(
            request.DateFrom,
            request.DateTo,
            request.DocumentNumbers,
            request.ResourceIds,
            request.UnitIds);

        return [.. documents.Select(d => new ReceiptDocumentResponse
        {
            Id = d.Id,
            Number = d.Number,
            CreatedAt = d.CreatedAt,
            ReceiptResources = [.. d.ReceiptResources
                .Select(r => new ReceiptResourceResponse
                {
                    Id = r.Id,
                    ResourceId = r.ResourceId,
                    ResourceName = r.Resource.Name,
                    UnitId = r.UnitId,
                    UnitName = r.Unit.Name,
                    Quantity = r.Quantity
                })]
        })];
    }
}

