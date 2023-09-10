using AHutak.Rbac.Core.Abstractions.Entities.PermissionAggregate;
using AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;

namespace AHutak.Rbac.Core.Abstractions.Services;

public interface IReviewService<TRole, TPermission>
    where TRole : Role
    where TPermission : Permission
{
    Task<List<TRole>> GetAssignedRolesAsync(Guid userId, CancellationToken cancellationToken);
    Task<List<Guid>> GetAssignedUsersAsync(Guid roleId, CancellationToken cancellationToken);
    Task<List<TPermission>> GetRolePermissionsAsync(Guid roleId, CancellationToken cancellationToken);
    Task<List<TPermission>> GetUserPermissionsAsync(Guid userId, CancellationToken cancellationToken);
}
