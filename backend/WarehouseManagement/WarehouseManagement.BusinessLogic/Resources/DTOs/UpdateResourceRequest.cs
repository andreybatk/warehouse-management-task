namespace WarehouseManagement.BusinessLogic.Resources.DTOs;

/// <summary>
/// Запрос на обновление ресурса
/// </summary>
public class UpdateResourceRequest
{
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; init; } = string.Empty;
}