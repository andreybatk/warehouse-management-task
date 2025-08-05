using WarehouseManagement.BusinessLogic.Abstractions.Messaging;
using WarehouseManagement.BusinessLogic.Resources.DTOs;

namespace WarehouseManagement.BusinessLogic.Resources.Queries;
/// <summary>
/// Запрос на получение ресурса
/// </summary>
/// <param name="Id">Идентификатор ресурса</param>
public sealed record GetResourceQuery(Guid Id) : IQuery<ResourceResponse>;
