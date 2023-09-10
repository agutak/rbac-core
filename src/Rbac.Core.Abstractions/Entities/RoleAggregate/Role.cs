using AHutak.Rbac.Core.Abstractions.Entities.PermissionAggregate;

namespace AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;

public class Role
{
    protected readonly List<UserAssignment> _userAssignments = new();
    protected readonly List<PermissionAssignment> _permissionAssignments = new();

    public Role(
        Guid id,
        string name,
        string? description)
    {
        ValidateName(name);

        Id = id;
        Name = name;
        Description = description;
    }

    public Guid Id { get; init; }
    public string Name { get; protected set; }
    public string? Description { get; protected set; }
    public IReadOnlyList<UserAssignment> UserAssignments =>
        _userAssignments.AsReadOnly();
    public IReadOnlyList<PermissionAssignment> PermissionAssignments =>
        _permissionAssignments.AsReadOnly();

    public virtual void UpdateInfo(string name, string? description)
    {
        ValidateName(name);

        Name = name;
        Description = description;
    }

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

    public virtual void GrantPermission(Permission permission)
    {
        if (_permissionAssignments.Any(x => x.PermissionId == permission.Id))
            return;

        _permissionAssignments.Add(new PermissionAssignment(permission.Id, Id));
    }

    public virtual void RevokePermission(Guid permissionId)
    {
        _ = _permissionAssignments.RemoveAll(x => x.PermissionId == permissionId);
    }

    public virtual void RevokePermission(Permission permission)
    {
        _ = _permissionAssignments.RemoveAll(x => x.PermissionId == permission.Id);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role Name cannot be null or whitespace", nameof(name));
    }
}
