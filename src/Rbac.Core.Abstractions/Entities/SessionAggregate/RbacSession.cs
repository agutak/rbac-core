namespace AHutak.Rbac.Core.Abstractions.Entities;

public class RbacSession : Entity
{
    private readonly List<RbacSessionRole> _rbacSessionRoles = [];

    private RbacSession() { }

    public RbacSession(Guid id, string name, string? description, Guid userId)
        : base(id, name, description)
    {
        UserId = userId;
    }

    public Guid UserId { get; init; }

    public IReadOnlyList<RbacSessionRole> RbacSessionRoles =>
        _rbacSessionRoles.AsReadOnly();

    private void AddRoles(List<Role> roles)
    {
        foreach (var role in roles)
            _rbacSessionRoles.Add(new(Id, role.Id));
    }

    public void AddActiveRole(Guid roleId)
    {
        if (_rbacSessionRoles.Any(x => x.RoleId == roleId))
            return;

        _rbacSessionRoles.Add(new(Id, roleId));
    }

    public void DropActiveRole(Guid roleId)
    {
        var role = _rbacSessionRoles.FirstOrDefault(x => x.RoleId == roleId);

        if (role is null)
            return;

        _rbacSessionRoles.Remove(role);
    }

    public static RbacSession Create(Guid id, string name, string? description, Guid userId, List<Role> roles)
    {
        var session = new RbacSession(id, name, description, userId);
        session.AddRoles(roles);
        return session;
    }
}
