namespace AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;

public class UserAssignment
{
    public UserAssignment(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public Guid UserId { get; init; }
    public Guid RoleId { get; init; }
}
