using AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHutak.Rbac.Core.Persistence.MsSql.Context.EntitiesConfigurations;

internal class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    private const string _keyPropertyName = "_id";

    public void Configure(EntityTypeBuilder<Role> builder)
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
            .IsRequired();

        builder.Metadata
            .FindNavigation(nameof(Role.UserAssignments))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(x => x.UserAssignments)
            .WithOne()
            .IsRequired();

        builder
            .HasIndex(x => x.Id)
            .IsUnique();
    }
}
