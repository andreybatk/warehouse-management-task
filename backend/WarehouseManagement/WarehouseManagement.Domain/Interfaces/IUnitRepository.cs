using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Domain.Interfaces;

public interface IUnitRepository
{
    /// <summary>
    /// Получить все единицы измерения
    /// </summary>
    /// <param name="state">Состояние</param>
    /// <returns></returns>
    Task<List<Unit>> GetAllAsync(EState? state = null);

    /// <summary>
    /// Получить единицу измерения по идентификатору
    /// </summary>
    Task<Unit?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить новую единицу измерения
    /// </summary>
    Task<Guid> AddAsync(Unit unit);

    /// <summary>
    /// Обновить существующую единицу измерения
    /// </summary>
    Task<Guid> UpdateAsync(Unit unit);

    /// <summary>
    /// Удалить единицу измерения
    /// </summary>
    Task<Guid?> DeleteAsync(Guid id);

    /// <summary>
    /// Проверить, существует ли единица измерения с таким именем.
    /// </summary>
    Task<bool> ExistsByNameAsync(string name);

    /// <summary>
    /// Проверить, существует ли единица измерения с таким идентификатором.
    /// </summary>
    Task<bool> ExistsByIdAsync(Guid id);

    /// <summary>
    /// Получить единицу измерения по имени
    /// </summary>
    Task<Unit?> GetByNameAsync(string name);
}
