namespace AHutak.Rbac.Core.Abstractions.Entities;

public class UserAssignment
{
    public UserAssignment(string userId, Guid roleId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId cannot be null or whitespace", nameof(userId));

        UserId = userId;
        RoleId = roleId;
    }

    public string UserId { get; init; }
    public Guid RoleId { get; init; }
}
