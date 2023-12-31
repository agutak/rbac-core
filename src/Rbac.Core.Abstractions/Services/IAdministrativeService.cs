﻿using AHutak.Rbac.Core.Abstractions.Entities;

namespace AHutak.Rbac.Core.Abstractions.Services;

public interface IAdministrativeService<TRole> where TRole : Role
{
    Task AddRoleAsync(TRole role, CancellationToken cancellationToken);
    Task<bool> DeleteRoleAsync(Guid roleId, CancellationToken cancellationToken);
    Task<bool> AssignUserAsync(string userId, Guid roleId, CancellationToken cancellationToken);
    Task<bool> DeassignUserAsync(string userId, Guid roleId, CancellationToken cancellationToken);
    Task<bool> GrantPermissionAsync(Guid permissionId, Guid roleId, CancellationToken cancellationToken);
    Task<bool> RevokePermissionAsync(Guid permissionId, Guid roleId, CancellationToken cancellationToken);
}
