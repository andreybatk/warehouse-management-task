using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.DTOs;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Resources.Queries;

public sealed record GetResourcesQuery(State? State) : IQuery<List<ResourceResponse>>;