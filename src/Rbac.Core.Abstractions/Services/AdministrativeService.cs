using AHutak.Rbac.Core.Abstractions.Entities;

namespace AHutak.Rbac.Core.Abstractions.Services;

public class AdministrativeService : AdministrativeService<Role, RbacSession>
{
    public AdministrativeService(IRolesRepository<Role> rolesRepository, IRbacSessionsRepository<RbacSession> rbacSessionsRepository)
        : base(rolesRepository, rbacSessionsRepository)
    {
    }
}

public class AdministrativeService<TRole, TSession> : IAdministrativeService<TRole, TSession>
    where TRole : Role
    where TSession : RbacSession
{
    private readonly IRolesRepository<TRole> _rolesRepository;
    private readonly IRbacSessionsRepository<TSession> _rbacSessionsRepository;

    public AdministrativeService(IRolesRepository<TRole> rolesRepository, IRbacSessionsRepository<TSession> rbacSessionsRepository)
    {
        _rolesRepository = rolesRepository;
        _rbacSessionsRepository = rbacSessionsRepository;
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var roles = await _rolesRepository
            .GetAssignedRolesAsync(userId, cancellationToken)
            .ConfigureAwait(false);

        if (roles.Count == 0)
            return;

        foreach (var role in roles)
            role.DeassignUser(userId);

        await _rolesRepository
            .SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
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

    public async Task<bool> AssignUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
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

    public async Task<bool> DeassignUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken)
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

    public async Task CreateSessionAsync(TSession rbacSession, CancellationToken cancellationToken)
    {
        await _rbacSessionsRepository
            .AddAsync(rbacSession, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<bool> DeleteSessionAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken)
    {
        var rbacSession = await _rbacSessionsRepository
            .GetAggregateAsync(sessionId, cancellationToken)
            .ConfigureAwait(false);

        if (rbacSession is null || rbacSession.UserId != userId)
            return false;

        await _rbacSessionsRepository
            .DeleteAsync(rbacSession, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }

    public async Task<bool> AddActiveRoleAsync(Guid sessionId, Guid userId, TRole role, CancellationToken cancellationToken)
    {
        var rbacSession = await _rbacSessionsRepository
            .GetAggregateAsync(sessionId, cancellationToken)
            .ConfigureAwait(false);

        if (rbacSession is null || rbacSession.UserId != userId)
            return false;

        rbacSession.AddActiveRole(role.Id);

        await _rbacSessionsRepository
            .UpdateAsync(rbacSession, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }

    public async Task<bool> DropActiveRoleAsync(Guid sessionId, Guid userId, TRole role, CancellationToken cancellationToken)
    {
        var rbacSession = await _rbacSessionsRepository
            .GetAggregateAsync(sessionId, cancellationToken)
            .ConfigureAwait(false);

        if (rbacSession is null || rbacSession.UserId != userId)
            return false;

        rbacSession.DropActiveRole(role.Id);

        await _rbacSessionsRepository
            .UpdateAsync(rbacSession, cancellationToken)
            .ConfigureAwait(false);

        return true;
    }

    public async Task<bool> CheckAccessAsync(Guid sessionId, Guid operationId, Guid objectId, CancellationToken cancellationToken)
    {
        // TODO: Implement
        return true;
    }
}
