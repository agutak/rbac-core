using AHutak.Rbac.Core.Abstractions.Entities.PermissionAggregate;
using AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;

namespace AHutak.Rbac.Core.Abstractions.Services;

public class ReviewService<TRole, TPermission> : IReviewService<TRole, TPermission>
    where TRole : Role
    where TPermission : Permission
{
    private readonly IRolesRepository<TRole> _rolesRepository;

    public ReviewService(IRolesRepository<TRole> rolesRepository)
    {
        _rolesRepository = rolesRepository;
    }

    public async Task<List<string>> GetAssignedUsersAsync(Guid roleId, CancellationToken cancellationToken)
    {
        return await _rolesRepository
            .GetAssignedUsersAsync(roleId, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<List<TRole>> GetAssignedRolesAsync(string userId, CancellationToken cancellationToken)
    {
        return await _rolesRepository
            .GetAssignedRolesAsync(userId, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<List<TPermission>> GetRolePermissionsAsync(Guid roleId, CancellationToken cancellationToken)
    {
        return await _rolesRepository
            .GetRolePermissionsAsync<TPermission>(roleId, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<List<TPermission>> GetUserPermissionsAsync(string userId, CancellationToken cancellationToken)
    {
        return await _rolesRepository
            .GetUserPermissionsAsync<TPermission>(userId, cancellationToken)
            .ConfigureAwait(false);
    }
}
