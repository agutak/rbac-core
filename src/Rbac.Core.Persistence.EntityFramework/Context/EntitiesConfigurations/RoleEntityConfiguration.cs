using AHutak.Rbac.Core.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHutak.Rbac.Core.Persistence.EntityFramework;

internal class RoleEntityConfiguration<TRole> : IEntityTypeConfiguration<TRole>
    where TRole : Role
{
    private static readonly string _keyPropertyName = "_id";

    public void Configure(EntityTypeBuilder<TRole> builder)
    {
        builder.ToTable("Roles");

        builder.Property<int>(_keyPropertyName);

        builder.HasKey(_keyPropertyName);

        builder.Metadata
            .FindNavigation(nameof(Role.PermissionAssignments))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(x => x.PermissionAssignments)
            .WithOne()
            .HasPrincipalKey(x => x.Id)
            .IsRequired();

        builder.Metadata
            .FindNavigation(nameof(Role.UserAssignments))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(x => x.UserAssignments)
            .WithOne()
            .HasPrincipalKey(x => x.Id)
            .IsRequired();

        builder
            .HasIndex(x => x.Id)
            .IsUnique();
    }
}
