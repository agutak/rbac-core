using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AHutak.Rbac.Core.Persistence.MsSql.Context;

public class RbacDbContext : DbContext
{
    public const string DefaultSchema = "rbac";

    public RbacDbContext(DbContextOptions<RbacDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
