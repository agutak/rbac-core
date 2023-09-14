namespace AHutak.Rbac.Core.Abstractions.Entities;

public class OperationPermission : Permission
{
    public OperationPermission(
        Guid id,
        string name,
        string? description,
        Guid operationId)
        : base(id, name, description)
    {
        OperationId = operationId;
    }

    public Guid OperationId { get; init; }
}
