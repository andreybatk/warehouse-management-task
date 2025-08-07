using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.Commands;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Handlers;

public class UpdateReceiptDocumentHandler(
    IReceiptDocumentRepository receiptDocumentRepository,
    ILogger<UpdateReceiptDocumentHandler> logger
) : ICommandHandler<UpdateReceiptDocumentCommand, Guid>
{
    public async Task<Guid> Handle(UpdateReceiptDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await receiptDocumentRepository.GetByIdAsync(request.Id, true);

        if (document is null)
        {
            logger.LogError("Документ поступления '{id}' не найден.", request.Id);
            throw new NotFoundException("Документ поступления не найден.");
        }

        document.Number = request.Number;
        document.CreatedAt = request.CreatedAt;

        var incomingResourceIds = request.ReceiptResources
            .Where(x => x.Id.HasValue)
            .Select(x => x.Id!.Value)
            .ToList();

        document.ReceiptResources.RemoveAll(x => !incomingResourceIds.Contains(x.Id));

        foreach (var resource in request.ReceiptResources)
        {
            if (resource.Id.HasValue)
            {
                var existing = document.ReceiptResources.FirstOrDefault(x => x.Id == resource.Id.Value);
                if (existing is not null)
                {
                    existing.ResourceId = resource.ResourceId;
                    existing.UnitId = resource.UnitId;
                    existing.Quantity = resource.Quantity;
                }
            }
            else
            {
                document.ReceiptResources.Add(new ReceiptResource
                {
                    ResourceId = resource.ResourceId,
                    UnitId = resource.UnitId,
                    Quantity = resource.Quantity,
                    ReceiptDocumentId = document.Id
                });
            }
        }

        var updatedId = await receiptDocumentRepository.UpdateAsync(document);
        return updatedId;
    }
}