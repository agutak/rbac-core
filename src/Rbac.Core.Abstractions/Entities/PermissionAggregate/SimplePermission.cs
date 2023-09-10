namespace AHutak.Rbac.Core.Abstractions.Entities.PermissionAggregate;

public class SimplePermission : Permission
{
    public SimplePermission(
        Guid id,
        string name,
        string? description)
        : base(id, name, description)
    {

    }
}
