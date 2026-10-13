namespace AHutak.Rbac.Core.Abstractions.Entities;

public abstract class Permission(Guid id, string name, string? description) : Entity(id, name, description)
{

}
