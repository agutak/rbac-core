using AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHutak.Rbac.Core.Persistence.MsSql.Context.EntitiesConfigurations;

internal class UserAssignmentEntityConfiguration : IEntityTypeConfiguration<UserAssignment>
{
    public void Configure(EntityTypeBuilder<UserAssignment> builder)
    {
        builder.ToTable("UserAssignments");

        builder.HasKey(x => new { x.RoleId, x.UserId });
    }
}
