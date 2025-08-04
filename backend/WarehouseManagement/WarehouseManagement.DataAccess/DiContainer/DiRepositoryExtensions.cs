using Microsoft.Extensions.DependencyInjection;
using WarehouseManagement.DataAccess.Repositories;
using WarehouseManagement.Domain.Interfaces;

namespace WarehouseManagement.DataAccess.DiContainer;

public static class DiRepositoryExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<IUnitRepository, UnitRepository>();
        services.AddScoped<IReceiptDocumentRepository, ReceiptDocumentRepository>();
    }
}
