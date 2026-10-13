namespace AHutak.Rbac.Core.Abstractions.Entities;

public class BaseRbacOperation : IRbacOperation
{
    protected BaseRbacOperation() { }

    public BaseRbacOperation(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}
