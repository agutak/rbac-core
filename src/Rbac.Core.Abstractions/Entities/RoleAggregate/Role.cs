namespace AHutak.Rbac.Core.Abstractions.Entities;

public class Role(Guid id, string name, string? description) : Entity(id, name, description)
{
    private readonly List<UserAssignment> _userAssignments = [];
    private readonly List<PermissionAssignment> _permissionAssignments = [];

    public IReadOnlyList<UserAssignment> UserAssignments =>
        _userAssignments.AsReadOnly();
    public IReadOnlyList<PermissionAssignment> PermissionAssignments =>
        _permissionAssignments.AsReadOnly();

    public virtual void AssignUser(Guid userId)
    {
        if (_userAssignments.Any(x => x.UserId == userId))
            return;

        _userAssignments.Add(new UserAssignment(userId, Id));
    }

    public virtual void DeassignUser(Guid userId)
    {
        _ = _userAssignments.RemoveAll(x => x.UserId == userId);
    }

    public virtual void GrantPermission(Guid permissionId)
    {
        if (_permissionAssignments.Any(x => x.PermissionId == permissionId))
            return;

        _permissionAssignments.Add(new PermissionAssignment(permissionId, Id));
    }

    public virtual void RevokePermission(Guid permissionId)
    {
        _ = _permissionAssignments.RemoveAll(x => x.PermissionId == permissionId);
    }
}
