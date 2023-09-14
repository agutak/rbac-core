using AHutak.Rbac.Core.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace AHutak.Rbac.Core.Persistence.EntityFramework;

public class RbacDbContext : RbacDbContext<Role, SimplePermission>
{
    public RbacDbContext(DbContextOptions<RbacDbContext> options) : base(options) { }

    protected RbacDbContext(DbContextOptions options) : base(options) { }
}

public abstract class RbacDbContext<TRole, TPermission> : DbContext
    where TRole : Role
    where TPermission : Permission
{
    public const string DefaultSchema = "rbac";

    public RbacDbContext(DbContextOptions<RbacDbContext<TRole, TPermission>> options) : base(options) { }

    protected RbacDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema);

        modelBuilder.ApplyConfiguration(new UserAssignmentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionAssignmentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityConfiguration<TRole>());
        modelBuilder.ApplyConfiguration(new PermissionEntityConfiguration<TPermission>());
    }
}
