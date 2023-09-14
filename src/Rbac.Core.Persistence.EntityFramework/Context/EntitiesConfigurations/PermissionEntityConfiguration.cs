using AHutak.Rbac.Core.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHutak.Rbac.Core.Persistence.EntityFramework;

internal sealed class PermissionEntityConfiguration<TPermission> : IEntityTypeConfiguration<TPermission>
    where TPermission : Permission
{
    private static readonly string _keyPropertyName = "_id";

    public void Configure(EntityTypeBuilder<TPermission> builder)
    {
        builder.ToTable("Permissions");

        builder.Property<int>(_keyPropertyName);

        builder.HasKey(_keyPropertyName);

        builder
            .HasIndex(x => x.Id)
            .IsUnique();
    }
}
