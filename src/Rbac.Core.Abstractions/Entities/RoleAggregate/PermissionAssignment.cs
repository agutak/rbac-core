﻿namespace AHutak.Rbac.Core.Abstractions.Entities;

public class PermissionAssignment
{
    public PermissionAssignment(Guid permissionId, Guid roleId)
    {
        PermissionId = permissionId;
        RoleId = roleId;
    }

    public Guid PermissionId { get; init; }
    public Guid RoleId { get; init; }
}
