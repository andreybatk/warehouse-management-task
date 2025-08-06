using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.Commands;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Handlers;

public class UpdateReceiptDocumentHandler(IReceiptDocumentRepository receiptDocumentRepository, ILogger<UpdateReceiptDocumentHandler> logger)
    : ICommandHandler<UpdateReceiptDocumentCommand, Guid>
{
    public async Task<Guid> Handle(UpdateReceiptDocumentCommand request, CancellationToken cancellationToken)
    {
        var document = await receiptDocumentRepository.GetByIdAsync(request.Id);

        if (document is null)
        {
            logger.LogError("Документ поступления '{id}' не найден.", request.Id);
            throw new NotFoundException("Документ поступления не найден.");
        }

        document.Number = request.Number;
        document.CreatedAt = request.CreatedAt;

        var updatedId = await receiptDocumentRepository.UpdateAsync(document);

        return updatedId;
    }
}

