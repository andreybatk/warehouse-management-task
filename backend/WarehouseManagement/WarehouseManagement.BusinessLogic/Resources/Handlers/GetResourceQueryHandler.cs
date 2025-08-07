using Microsoft.Extensions.Logging;
using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.DTOs;
using WarehouseManagement.BusinessLogic.Resources.Queries;
using WarehouseManagement.Domain.Exceptions;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.BusinessLogic.Resources.Handlers;
public class GetResourceQueryHandler(IResourceRepository resourceRepository, ILogger<GetResourceQueryHandler> logger)
    : IQueryHandler<GetResourceQuery, ResourceResponse>
{
    public async Task<ResourceResponse> Handle(GetResourceQuery request, CancellationToken cancellationToken)
    {
        var resource = await resourceRepository.GetByIdAsync(request.Id);

        if (resource is null)
        {
            logger.LogError("Ресурс '{id}' не найден.", request.Id);
            throw new NotFoundException("Ресурс не найден.");
        }

        return new ResourceResponse(resource.Id, resource.Name, resource.State); //TODO: сделать mapper
    }
}
