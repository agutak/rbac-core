namespace AHutak.Rbac.Core.Abstractions.Entities;

public class OperationOnObjectPermission : Permission
{
    public OperationOnObjectPermission(
        Guid id,
        string name,
        string? description,
        Guid operationId,
        Guid objectId)
        : base(id, name, description)
    {
        OperationId = operationId;
        ObjectId = objectId;
    }

    public OperationOnObjectPermission(
        Guid id,
        string name,
        string? description,
        IRbacOperation rbacOperation,
        IRbacObject rbacObject)
        : base(id, name, description)
    {
        ArgumentNullException.ThrowIfNull(rbacOperation, nameof(rbacOperation));
        ArgumentNullException.ThrowIfNull(rbacObject, nameof(rbacObject));

        OperationId = rbacOperation.Id;
        ObjectId = rbacObject.Id;
    }

    public Guid OperationId { get; init; }
    public Guid ObjectId { get; init; }
}
