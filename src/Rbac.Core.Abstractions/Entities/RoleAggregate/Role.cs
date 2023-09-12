namespace AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;

public class Role
{
    private readonly List<UserAssignment> _userAssignments = new();
    private readonly List<PermissionAssignment> _permissionAssignments = new();

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

    public virtual void AssignUser(string userId)
    {
        if (_userAssignments.Any(x => x.UserId == userId))
            return;

        _userAssignments.Add(new UserAssignment(userId, Id));
    }

    public virtual void DeassignUser(string userId)
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

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role Name cannot be null or whitespace", nameof(name));
    }
}
