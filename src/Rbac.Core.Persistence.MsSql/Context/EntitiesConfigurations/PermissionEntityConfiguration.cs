using AHutak.Rbac.Core.Abstractions.Entities.PermissionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHutak.Rbac.Core.Persistence.MsSql.Context.EntitiesConfigurations;

internal class PermissionEntityConfiguration : IEntityTypeConfiguration<SimplePermission>
{
    private const string _keyPropertyName = "_id";

    public void Configure(EntityTypeBuilder<SimplePermission> builder)
    {
        builder.ToTable("PermissionAssignments");

        builder.Property<int>(_keyPropertyName);

        builder.HasKey(_keyPropertyName);

        builder
            .HasIndex(x => x.Id)
            .IsUnique();
    }
}
