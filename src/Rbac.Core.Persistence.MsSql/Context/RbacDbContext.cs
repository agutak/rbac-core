using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AHutak.Rbac.Core.Persistence.MsSql.Context;

internal class RbacDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "rbac";

    public RbacDbContext(DbContextOptions<RbacDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DEFAULT_SCHEMA);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
