﻿namespace AHutak.Rbac.Core.Abstractions.Entities;

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
