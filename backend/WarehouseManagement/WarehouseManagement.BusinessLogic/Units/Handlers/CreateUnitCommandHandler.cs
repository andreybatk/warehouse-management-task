using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Units.Commands;
using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Units.Handlers;

public class CreateUnitCommandHandler(IUnitRepository unitRepository, ILogger<CreateUnitCommandHandler> logger)
    : ICommandHandler<CreateUnitCommand, Guid>
{
    public async Task<Guid> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
    {
        if (await unitRepository.ExistsByNameAsync(request.Name))
        {
            logger.LogError("Единица измерения с таким наименованием '{name}' уже существует.", request.Name);
            throw new BadRequestException("Единица измерения с таким наименованием уже существует.");
        }

        var unit = new Unit { Name = request.Name, State = request.State };

        return await unitRepository.AddAsync(unit);
    }
}