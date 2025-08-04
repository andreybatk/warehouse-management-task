using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Units.Commands;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Units.Handlers;

public class DeleteUnitCommandHandler(IUnitRepository unitRepository, ILogger<DeleteUnitCommandHandler> logger)
    : ICommandHandler<DeleteUnitCommand, Guid?>
{
    public async Task<Guid?> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
    {
        if (await unitRepository.ExistsByIdAsync(request.Id))
        {
            logger.LogError("Единица измерения '{id}' не найдена.", request.Id);
            throw new NotFoundException("Единица измерения не найдена.");
        }

        //TODO: проверка на использовании в документах поступления

        var deletedId = await unitRepository.DeleteAsync(request.Id);

        return deletedId;
    }
}