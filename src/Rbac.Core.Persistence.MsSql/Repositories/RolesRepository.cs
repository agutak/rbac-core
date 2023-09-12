using AHutak.Rbac.Core.Abstractions.Entities.PermissionAggregate;
using AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;
using AHutak.Rbac.Core.Abstractions.Services;
using AHutak.Rbac.Core.Persistence.MsSql.Context;
using Microsoft.EntityFrameworkCore;

namespace AHutak.Rbac.Core.Persistence.MsSql.Repositories;

internal class RolesRepository<TRole> : IRolesRepository<TRole> where TRole : Role
{
    private readonly RbacDbContext _dbContext;
    private readonly DbSet<TRole> _set;
    private readonly IQueryable<TRole> _readOnlySet;

    public RolesRepository(RbacDbContext dbContext)
    {
        _dbContext = dbContext;
        _set = dbContext.Set<TRole>();
        _readOnlySet = dbContext.Set<TRole>().AsNoTracking();
    }

    public Task AddAsync(TRole role, CancellationToken cancellationToken)
    {
        _set.Add(role);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid roleId, CancellationToken cancellationToken)
    {
        // TODO: Implement
        return Task.CompletedTask;
    }

    public async Task<TRole?> GetAggregateAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _set
            .Include(x => x.PermissionAssignments)
            .Include(x => x.UserAssignments)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<List<TRole>> GetAssignedRolesAsync(string userId, CancellationToken cancellationToken)
    {
        return await _readOnlySet
            .Where(x => x.UserAssignments.Any(u => u.UserId == userId))
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<List<string>> GetAssignedUsersAsync(Guid roleId, CancellationToken cancellationToken)
    {
        return await _readOnlySet
            .Where(x => x.Id == roleId)
            .SelectMany(x => x.UserAssignments)
            .Select(x => x.UserId)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<List<TPermission>> GetRolePermissionsAsync<TPermission>(Guid roleId, CancellationToken cancellationToken)
        where TPermission : Permission
    {
        var permissions = _dbContext.Set<TPermission>();

        return await _readOnlySet
            .Where(x => x.Id == roleId)
            .SelectMany(x => x.PermissionAssignments)
            .Join(
                permissions,
                pa => pa.PermissionId,
                p => p.Id,
                (pa, p) => p)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<List<TPermission>> GetUserPermissionsAsync<TPermission>(string userId, CancellationToken cancellationToken)
        where TPermission : Permission
    {
        var permissions = _dbContext.Set<TPermission>();

        return await _readOnlySet
            .Where(x => x.UserAssignments.Any(u => u.UserId == userId))
            .SelectMany(x => x.PermissionAssignments)
            .Join(
                permissions,
                pa => pa.PermissionId,
                p => p.Id,
                (pa, p) => p)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
