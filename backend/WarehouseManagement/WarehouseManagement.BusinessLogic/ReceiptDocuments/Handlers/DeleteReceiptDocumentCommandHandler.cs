using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.ReceiptDocuments.Commands;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.ReceiptDocuments.Handlers;
public class DeleteReceiptDocumentCommandHandler(IReceiptDocumentRepository documentRepository, ILogger<DeleteReceiptDocumentCommandHandler> logger)
    : ICommandHandler<DeleteReceiptDocumentCommand, Guid?>
{
    public async Task<Guid?> Handle(DeleteReceiptDocumentCommand request, CancellationToken cancellationToken)
    {
        if (!await documentRepository.ExistsByIdAsync(request.Id))
        {
            logger.LogError("Документ поступления '{id}' не найден.", request.Id);
            throw new NotFoundException("Документ поступления не найден.");
        }

        var deletedId = await documentRepository.DeleteAsync(request.Id);

        return deletedId;
    }
}