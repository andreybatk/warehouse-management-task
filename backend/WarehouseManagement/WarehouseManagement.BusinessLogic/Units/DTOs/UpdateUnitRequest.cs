namespace WarehouseManagement.BusinessLogic.Units.DTOs;

/// <summary>
/// Запрос на обновление единицы измерения
/// </summary>
public class UpdateUnitRequest
{
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; init; } = string.Empty;
}