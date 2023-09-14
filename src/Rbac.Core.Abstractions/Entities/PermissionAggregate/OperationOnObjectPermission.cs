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

    public Guid OperationId { get; init; }
    public Guid ObjectId { get; init; }
}
