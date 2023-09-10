using AHutak.Rbac.Core.Abstractions.Entities.PermissionAggregate;
using AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;
using AHutak.Rbac.Core.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AHutak.Rbac.Core.Abstractions.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCoreRbacServices(this IServiceCollection services)
    {
        services.AddScoped<IAdministrativeService<Role>, AdministrativeService<Role>>();
        services.AddScoped<IReviewService<Role, SimplePermission>, ReviewService<Role, SimplePermission>>();
    }
}
