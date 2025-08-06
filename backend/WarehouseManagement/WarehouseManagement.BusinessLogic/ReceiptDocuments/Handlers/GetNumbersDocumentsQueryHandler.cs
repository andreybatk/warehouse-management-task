using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.Queries;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Handlers;
public class GetNumbersDocumentsQueryHandler(IReceiptDocumentRepository receiptDocumentRepository)
    : IQueryHandler<GetNumbersDocumentsQuery, List<long>>
{
    public async Task<List<long>> Handle(GetNumbersDocumentsQuery request, CancellationToken cancellationToken)
    {
        return await receiptDocumentRepository.GetNumbersAsync();
    }
}