using WarehouseManagement.Domain.Entities;
using WarehouseManagement.Domain.Enums;

namespace WarehouseManagement.Domain.Interfaces;

public interface IResourceRepository
{
    /// <summary>
    /// Получить все ресурсы
    /// </summary>
    /// <param name="state">Состояние</param>
    /// <returns></returns>
    Task<List<Resource>> GetAllAsync(EState? state = null);

    /// <summary>
    /// Получить ресурс по идентификатору
    /// </summary>
    Task<Resource?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить новый ресурс
    /// </summary>
    Task<Guid> AddAsync(Resource resource);

    /// <summary>
    /// Обновить существующий ресурс
    /// </summary>
    Task<Guid> UpdateAsync(Resource resource);

    /// <summary>
    /// Удалить ресурс
    /// </summary>
    Task<Guid?> DeleteAsync(Guid id);

    /// <summary>
    /// Проверить, существует ли ресурс с таким именем.
    /// </summary>
    Task<bool> ExistsByNameAsync(string name);

    /// <summary>
    /// Получить ресурс по имени
    /// </summary>
    Task<Resource?> GetByNameAsync(string name);
}
