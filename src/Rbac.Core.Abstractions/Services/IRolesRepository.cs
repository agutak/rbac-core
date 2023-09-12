using AHutak.Rbac.Core.Abstractions.Entities.PermissionAggregate;
using AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;

namespace AHutak.Rbac.Core.Abstractions.Services;

public interface IRolesRepository<TRole> where TRole : Role
{
    Task<TRole?> GetAggregateAsync(Guid id, CancellationToken cancellationToken);

    Task AddAsync(TRole role, CancellationToken cancellationToken);

    Task DeleteAsync(Guid roleId, CancellationToken cancellationToken);

    Task<List<string>> GetAssignedUsersAsync(Guid roleId, CancellationToken cancellationToken);

    Task<List<TRole>> GetAssignedRolesAsync(string userId, CancellationToken cancellationToken);

    Task<List<TPermission>> GetRolePermissionsAsync<TPermission>(Guid roleId, CancellationToken cancellationToken)
        where TPermission : Permission;

    Task<List<TPermission>> GetUserPermissionsAsync<TPermission>(string userId, CancellationToken cancellationToken)
        where TPermission : Permission;
}
