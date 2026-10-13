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

    public OperationPermission(
        Guid id,
        string name,
        string? description,
        IRbacOperation rbacOperation)
        : base(id, name, description)
    {
        ArgumentNullException.ThrowIfNull(rbacOperation, nameof(rbacOperation));
        OperationId = rbacOperation.Id;
    }

    public Guid OperationId { get; init; }
}
