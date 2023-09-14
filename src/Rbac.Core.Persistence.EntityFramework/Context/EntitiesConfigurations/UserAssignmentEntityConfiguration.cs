using AHutak.Rbac.Core.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AHutak.Rbac.Core.Persistence.EntityFramework;

internal class UserAssignmentEntityConfiguration : IEntityTypeConfiguration<UserAssignment>
{
    private static readonly string _keyPropertyName = "_id";

    public void Configure(EntityTypeBuilder<UserAssignment> builder)
    {
        builder.ToTable("UserAssignments");

        builder.Property<int>(_keyPropertyName);

        builder.HasKey(_keyPropertyName);
    }
}
