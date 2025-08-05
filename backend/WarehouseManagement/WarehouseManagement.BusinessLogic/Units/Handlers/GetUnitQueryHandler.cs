using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Units.DTOs;
using WarehouseManagement.BusinessLogic.Units.Queries;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Units.Handlers;

public class GetUnitQueryHandler(IUnitRepository unitRepository, ILogger<GetUnitQueryHandler> logger)
    : IQueryHandler<GetUnitQuery, UnitResponse>
{
    public async Task<UnitResponse> Handle(GetUnitQuery request, CancellationToken cancellationToken)
    {
        var unit = await unitRepository.GetByIdAsync(request.Id);

        if (unit is null)
        {
            logger.LogError("Единица измерения '{id}' не найдена.", request.Id);
            throw new NotFoundException("Единица измерения не найдена.");
        }

        return new UnitResponse(unit.Id, unit.Name, unit.State); //TODO: сделать маппер
    }
}
