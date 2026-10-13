namespace AHutak.Rbac.Core.Abstractions.Entities;

public class BaseRbacObject : IRbacObject
{
    protected BaseRbacObject() { }

    public BaseRbacObject(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}
