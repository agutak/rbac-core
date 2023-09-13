using AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;

namespace AHutak.Rbac.Core.Abstractions.Services;

public class AdministrativeService<TRole> : IAdministrativeService<TRole> where TRole : Role
{
    private readonly IRolesRepository<TRole> _rolesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AdministrativeService(
        IRolesRepository<TRole> rolesRepository,
        IUnitOfWork unitOfWork)
    {
        _rolesRepository = rolesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddRoleAsync(TRole role, CancellationToken cancellationToken)
    {
        await _rolesRepository
            .AddAsync(role, cancellationToken)
            .ConfigureAwait(false);

        await _unitOfWork
            .CompleteAsync(cancellationToken)
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
            .DeleteAsync(roleId, cancellationToken)
            .ConfigureAwait(false);

        await _unitOfWork
            .CompleteAsync(cancellationToken)
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

        await _unitOfWork
            .CompleteAsync(cancellationToken)
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

        await _unitOfWork
            .CompleteAsync(cancellationToken)
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

        await _unitOfWork
            .CompleteAsync(cancellationToken)
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

        await _unitOfWork
            .CompleteAsync(cancellationToken)
            .ConfigureAwait(false);

        return true;
    }
}
