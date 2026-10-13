using AHutak.Rbac.Core.Abstractions.Entities;

namespace AHutak.Rbac.Core.Abstractions.Services;

public interface IReviewService<TRole, TPermission, TOperation>
    where TRole : Role
    where TPermission : Permission
    where TOperation : IRbacOperation
{
    Task<List<TRole>> GetAssignedRolesAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<Guid>> GetAssignedUsersAsync(Guid roleId, CancellationToken cancellationToken);
    Task<List<TPermission>> GetRolePermissionsAsync(Guid roleId, CancellationToken cancellationToken);
    Task<List<TPermission>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<TRole>> GetSessionRolesAsync(Guid sessionId, CancellationToken cancellationToken);
    Task<List<TPermission>> GetSessionPermissionsAsync(Guid sessionId, CancellationToken cancellationToken);
    Task<List<TOperation>> GetRoleOperationsOnObjectAsync(Guid roleId, Guid objectId, CancellationToken cancellationToken);
    Task<List<TOperation>> GetUserOperationsOnObjectAsync(Guid roleId, Guid objectId, CancellationToken cancellationToken);
}
