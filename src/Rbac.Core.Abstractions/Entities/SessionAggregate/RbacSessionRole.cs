namespace AHutak.Rbac.Core.Abstractions.Entities;

public class RbacSessionRole
{
    public RbacSessionRole(Guid sessionId, Guid roleId)
    {
        SessionId = sessionId;
        RoleId = roleId;
    }

    public Guid SessionId { get; init; }
    public Guid RoleId { get; init; }
}
