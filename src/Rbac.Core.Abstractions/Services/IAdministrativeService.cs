using AHutak.Rbac.Core.Abstractions.Entities;

namespace AHutak.Rbac.Core.Abstractions.Services;

public interface IAdministrativeService<TRole, TSession>
    where TRole : Role
    where TSession : RbacSession
{
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
    Task AddRoleAsync(TRole role, CancellationToken cancellationToken);
    Task<bool> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken);
    Task<bool> AssignUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    Task<bool> DeassignUserAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
    Task<bool> GrantPermissionAsync(Guid permissionId, Guid roleId, CancellationToken cancellationToken);
    Task<bool> RevokePermissionAsync(Guid permissionId, Guid roleId, CancellationToken cancellationToken);
    Task CreateSessionAsync(TSession rbacSession, CancellationToken cancellationToken);
    Task<bool> DeleteSessionAsync(Guid sessionId, Guid userId, CancellationToken cancellationToken);
    Task<bool> AddActiveRoleAsync(Guid sessionId, Guid userId, TRole role, CancellationToken cancellationToken);
    Task<bool> DropActiveRoleAsync(Guid sessionId, Guid userId, TRole role, CancellationToken cancellationToken);
    Task<bool> CheckAccessAsync(Guid sessionId, Guid operationId, Guid objectId, CancellationToken cancellationToken);
}
