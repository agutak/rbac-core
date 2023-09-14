using AHutak.Rbac.Core.Abstractions.Entities;

namespace AHutak.Rbac.Core.Abstractions.Services;

public interface IReviewService<TRole, TPermission>
    where TRole : Role
    where TPermission : Permission
{
    Task<List<TRole>> GetAssignedRolesAsync(string userId, CancellationToken cancellationToken);
    Task<List<string>> GetAssignedUsersAsync(Guid roleId, CancellationToken cancellationToken);
    Task<List<TPermission>> GetRolePermissionsAsync(Guid roleId, CancellationToken cancellationToken);
    Task<List<TPermission>> GetUserPermissionsAsync(string userId, CancellationToken cancellationToken);
}
