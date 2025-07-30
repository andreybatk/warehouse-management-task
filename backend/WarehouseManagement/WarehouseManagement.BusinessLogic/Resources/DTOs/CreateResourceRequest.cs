using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.BusinessLogic.Resources.DTOs;

public class CreateResourceRequest
{
    public string Name { get; init; } = string.Empty;
    public State State { get; init; }
}
