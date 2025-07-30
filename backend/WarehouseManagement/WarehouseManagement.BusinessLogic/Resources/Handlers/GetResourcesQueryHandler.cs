using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.DTOs;
using WarehouseManagement.BusinessLogic.Resources.Queries;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Resources.Handlers;

public class GetResourcesQueryHandler : IQueryHandler<GetResourcesQuery, List<ResourceResponse>>
{
    private readonly IResourceRepository _resourceRepository;

    public GetResourcesQueryHandler(IResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public async Task<List<ResourceResponse>> Handle(GetResourcesQuery request, CancellationToken cancellationToken)
    {
        var resources = await _resourceRepository.GetAllAsync(request.State);
        return resources.Select(r => new ResourceResponse(r.Id, r.Name, r.State)).ToList();
    }
}
