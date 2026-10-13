namespace AHutak.Rbac.Core.Abstractions.Entities;

public class OperationsOnObjectsPermission : Permission
{
    private readonly List<IRbacOperation> _operations = [];
    private readonly List<IRbacObject> _objects = [];

    public OperationsOnObjectsPermission(
        Guid id,
        string name,
        string? description)
        : base(id, name, description) { }

    public OperationsOnObjectsPermission(
        Guid id,
        string name,
        string? description,
        List<IRbacOperation> rbacOperations,
        List<IRbacObject> rbacObjects)
        : this(id, name, description)
    {
        ArgumentNullException.ThrowIfNull(rbacOperations, nameof(rbacOperations));
        ArgumentNullException.ThrowIfNull(rbacObjects, nameof(rbacObjects));
    }

    public IReadOnlyList<IRbacOperation> Operations => _operations.AsReadOnly();

    public IReadOnlyList<IRbacObject> Objects => _objects.AsReadOnly();
}
