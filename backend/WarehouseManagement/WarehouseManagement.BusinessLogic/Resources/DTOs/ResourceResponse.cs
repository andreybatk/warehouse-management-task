using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Resources.DTOs;

public record ResourceResponse(Guid Id, string Name, State State);