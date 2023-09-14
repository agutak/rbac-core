using AHutak.Rbac.Core.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHutak.Rbac.Core.Persistence.EntityFramework;

internal class PermissionAssignmentEntityConfiguration : IEntityTypeConfiguration<PermissionAssignment>
{
    private static readonly string _keyPropertyName = "_id";

    public void Configure(EntityTypeBuilder<PermissionAssignment> builder)
    {
        builder.ToTable("PermissionAssignments");

        builder.Property<int>(_keyPropertyName);

        builder.HasKey(_keyPropertyName);
    }
}
