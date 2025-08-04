using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Units.DTOs;
using WarehouseManagement.BusinessLogic.Units.Queries;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Units.Handlers;

public class GetUnitsQueryHandler(IUnitRepository unitRepository)
    : IQueryHandler<GetUnitsQuery, List<UnitResponse>>
{
    public async Task<List<UnitResponse>> Handle(GetUnitsQuery request, CancellationToken cancellationToken)
    {
        var units = await unitRepository.GetAllAsync(request.State);

        return units.Select(r => new UnitResponse(r.Id, r.Name, r.State)).ToList(); //TODO: сделать маппер
    }
}
