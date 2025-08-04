using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.DTOs;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Resources.Queries;

/// <summary>
/// Запрос на получение всех ресурсов
/// </summary>
/// <param name="State"></param>
public sealed record GetResourcesQuery(EState? State) : IQuery<List<ResourceResponse>>;