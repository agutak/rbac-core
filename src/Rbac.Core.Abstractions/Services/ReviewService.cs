using AHutak.Rbac.Core.Abstractions.Entities;

namespace AHutak.Rbac.Core.Abstractions.Services;

public class ReviewService : ReviewService<Role, SimplePermission, BaseRbacOperation>
{
    public ReviewService(IRolesRepository<Role> rolesRepository) : base(rolesRepository)
    {
    }
}

public class ReviewService<TRole, TPermission, TOperation> : IReviewService<TRole, TPermission, TOperation>
    where TRole : Role
    where TPermission : Permission
    where TOperation : IRbacOperation
{
    private readonly IRolesRepository<TRole> _rolesRepository;

    public ReviewService(IRolesRepository<TRole> rolesRepository)
    {
        _rolesRepository = rolesRepository;
    }

    public async Task<List<Guid>> GetAssignedUsersAsync(Guid roleId, CancellationToken cancellationToken)
    {
        return await _rolesRepository
            .GetAssignedUsersAsync(roleId, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<List<TRole>> GetAssignedRolesAsync(Guid userId, CancellationToken cancellationToken)
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

    public async Task<List<TPermission>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _rolesRepository
            .GetUserPermissionsAsync<TPermission>(userId, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<List<TRole>> GetSessionRolesAsync(Guid sessionId, CancellationToken cancellationToken)
    {
        return [];
    }

    public async Task<List<TPermission>> GetSessionPermissionsAsync(Guid sessionId, CancellationToken cancellationToken)
    {
        return [];
    }

    public async Task<List<TOperation>> GetRoleOperationsOnObjectAsync(Guid roleId, Guid objectId, CancellationToken cancellationToken)
    {
        return [];
    }

    public async Task<List<TOperation>> GetUserOperationsOnObjectAsync(Guid roleId, Guid objectId, CancellationToken cancellationToken)
    {
        return [];
    }
}
