using AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHutak.Rbac.Core.Persistence.MsSql.Context.EntitiesConfigurations;

internal class PermissionAssignmentEntityConfiguration : IEntityTypeConfiguration<PermissionAssignment>
{
    public void Configure(EntityTypeBuilder<PermissionAssignment> builder)
    {
        builder.ToTable("PermissionAssignments");

        builder.HasKey(x => new { x.RoleId, x.PermissionId });
    }
}
