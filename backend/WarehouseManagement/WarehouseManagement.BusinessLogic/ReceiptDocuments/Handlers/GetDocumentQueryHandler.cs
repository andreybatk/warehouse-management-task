using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.DTOs;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.Queries;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Handlers;
public class GetDocumentQueryHandler(IReceiptDocumentRepository repository, ILogger<GetDocumentQueryHandler> logger)
    : IQueryHandler<GetDocumentQuery, ReceiptDocumentResponse>
{
    public async Task<ReceiptDocumentResponse> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
    {
        var document = await repository.GetByIdAsync(request.Id, true);

        if (document is null)
        {
            logger.LogError("Документ поступления '{id}' не найден.", request.Id);
            throw new NotFoundException("Документ поступления не найден.");
        }

        return new ReceiptDocumentResponse
        {
            Id = document.Id,
            Number = document.Number,
            CreatedAt = document.CreatedAt,
            ReceiptResources = [.. document.ReceiptResources
                .Select(r => new ReceiptResourceResponse
                {
                    Id = r.Id,
                    ResourceName = r.Resource.Name,
                    UnitName = r.Unit.Name,
                    Quantity = r.Quantity
                })]
        };
    }
}