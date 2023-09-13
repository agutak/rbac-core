using AHutak.Rbac.Core.Abstractions.Entities.PermissionAggregate;
using AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;
using AHutak.Rbac.Core.Abstractions.Services;
using AHutak.Rbac.Core.Persistence.MsSql.Context;
using AHutak.Rbac.Core.Persistence.MsSql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AHutak.Rbac.Core.Persistence.MsSql.Extensions;

public static class ServiceCollectionExtensions
{
    public const string DbConnectionStringName = "RbacDb";

    public static void AddCoreRbacMsSqlPersistenceServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnectionString = configuration.GetConnectionString(DbConnectionStringName);

        AddCoreRbacMsSqlPersistenceServices(services, dbConnectionString);
    }

    public static void AddCoreRbacMsSqlPersistenceServices(
        this IServiceCollection services, string dbConnectionString)
    {
        AddCoreRbacMsSqlPersistenceServices<Role, SimplePermission>(services, dbConnectionString);
    }

    public static void AddCoreRbacMsSqlPersistenceServices<TRole, TPermission>(
        this IServiceCollection services, IConfiguration configuration)
        where TRole : Role
        where TPermission : Permission
    {
        var dbConnectionString = configuration.GetConnectionString(DbConnectionStringName);

        AddCoreRbacMsSqlPersistenceServices<TRole, TPermission>(services, dbConnectionString);
    }

    public static void AddCoreRbacMsSqlPersistenceServices<TRole, TPermission>(
        this IServiceCollection services, string dbConnectionString)
        where TRole : Role
        where TPermission : Permission
    {
        if (dbConnectionString is null || dbConnectionString.Length <= 0)
            throw new ArgumentNullException(nameof(dbConnectionString), "DB connection string cannot be null.");

        services.AddDbContext<RbacDbContext>(builder =>
            builder.UseSqlServer(
                dbConnectionString,
                options => options.EnableRetryOnFailure()));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRolesRepository<TRole>, RolesRepository<TRole>>();
    }
}
