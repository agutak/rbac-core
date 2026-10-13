namespace AHutak.Rbac.Core.Abstractions.Entities;

public abstract class Entity
{
    protected Entity() { }

    protected Entity(Guid id, string name, string? description)
    {
        ValidateName(name);

        Id = id;
        Name = name;
        Description = description;
    }

    public Guid Id { get; init; }
    public string Name { get; protected set; }
    public string? Description { get; protected set; }

    public virtual void UpdateInfo(string name, string? description)
    {
        ValidateName(name);

        Name = name;
        Description = description;
    }

    private static void ValidateName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
    }
}
