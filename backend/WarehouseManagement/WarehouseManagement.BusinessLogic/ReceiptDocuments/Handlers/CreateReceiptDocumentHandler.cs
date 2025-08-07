using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.Commands;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Handlers;
public class CreateReceiptDocumentHandler(IReceiptDocumentRepository documentRepository, ILogger<CreateReceiptDocumentHandler> logger)
    : ICommandHandler<CreateReceiptDocumentCommand, Guid>
{
    public async Task<Guid> Handle(CreateReceiptDocumentCommand request, CancellationToken cancellationToken)
    {
        if (await documentRepository.ExistsByNumberAsync(request.Number))
        {
            logger.LogError("Документ поступления с таким номером '{number}' уже существует.", request.Number);
            throw new BadRequestException("Документ поступления с таким номером уже существует.");
        }

        var document = new ReceiptDocument
        {
            Number = request.Number,
            CreatedAt = request.CreatedAt,
            ReceiptResources = request.ReceiptResources != null
                ? request.ReceiptResources.Select(r => new ReceiptResource
                {
                    ResourceId = r.ResourceId,
                    UnitId = r.UnitId,
                    Quantity = r.Quantity
                }).ToList()
                : []
        };

        await documentRepository.AddAsync(document);

        return document.Id;
    }
}
