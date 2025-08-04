using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Units.Commands;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Units.Handlers;

public class ChangeUnitStateHandler(IUnitRepository unitRepository, ILogger<ChangeUnitStateHandler> logger)
    : ICommandHandler<ChangeUnitStateCommand, Guid>
{
    public async Task<Guid> Handle(ChangeUnitStateCommand request, CancellationToken cancellationToken)
    {
        var unit = await unitRepository.GetByIdAsync(request.Id);

        if (unit is null)
        {
            logger.LogError("Единица измерения '{id}' не найдена.", request.Id);
            throw new NotFoundException("Единица измерения не найдена.");
        }

        unit.State = request.NewState;

        var updatedId = await unitRepository.UpdateAsync(unit);

        return updatedId;
    }
}