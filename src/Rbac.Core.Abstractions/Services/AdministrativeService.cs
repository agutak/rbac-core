using AHutak.Rbac.Core.Abstractions.Entities;

namespace AHutak.Rbac.Core.Abstractions.Services;

public class AdministrativeService : AdministrativeService<Role>
{
    public AdministrativeService(IRolesRepository<Role> rolesRepository) : base(rolesRepository)
    {
    }
}

public class AdministrativeService<TRole> : IAdministrativeService<TRole>
    where TRole : Role
{
    private readonly IRolesRepository<TRole> _rolesRepository;

    public AdministrativeService(IRolesRepository<TRole> rolesRepository)
    {
        _rolesRepository = rolesRepository;
    }

    public async Task AddRoleAsync(TRole role, CancellationToken cancellationToken)
    {
        await _rolesRepository
            .AddAsync(role, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<bool> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken)
    {
        var role = await _rolesRepository
            .GetAggregateAsync(roleId, cancellationToken)
            .ConfigureAwait(false);

        if (role is null)
            return false;

        await _rolesRepository
            .DeleteAsync(role, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }

    public async Task<bool> AssignUserAsync(string userId, Guid roleId, CancellationToken cancellationToken)
    {
        var role = await _rolesRepository
            .GetAggregateAsync(roleId, cancellationToken)
            .ConfigureAwait(false);

        if (role is null)
            return false;

        role.AssignUser(userId);

        await _rolesRepository
            .UpdateAsync(role, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }

    public async Task<bool> DeassignUserAsync(string userId, Guid roleId, CancellationToken cancellationToken)
    {
        var role = await _rolesRepository
            .GetAggregateAsync(roleId, cancellationToken)
            .ConfigureAwait(false);

        if (role is null)
            return false;

        role.DeassignUser(userId);

        await _rolesRepository
            .UpdateAsync(role, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }

    public async Task<bool> GrantPermissionAsync(Guid permissionId, Guid roleId, CancellationToken cancellationToken)
    {
        var role = await _rolesRepository
            .GetAggregateAsync(roleId, cancellationToken)
            .ConfigureAwait(false);

        if (role is null)
            return false;

        role.GrantPermission(permissionId);

        await _rolesRepository
            .UpdateAsync(role, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }

    public async Task<bool> RevokePermissionAsync(Guid permissionId, Guid roleId, CancellationToken cancellationToken)
    {
        var role = await _rolesRepository
            .GetAggregateAsync(roleId, cancellationToken)
            .ConfigureAwait(false);

        if (role is null)
            return false;

        role.RevokePermission(permissionId);

        await _rolesRepository
            .UpdateAsync(role, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }
}
