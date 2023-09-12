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

    public static void RegisterMsSqlPersistenceServices<TRole>(
        this IServiceCollection services, IConfiguration configuration)
        where TRole : Role
    {
        var dbConnectionString = configuration.GetConnectionString(DbConnectionStringName);

        RegisterMsSqlPersistenceServices<TRole>(services, dbConnectionString);
    }

    public static void RegisterMsSqlPersistenceServices<TRole>(
        this IServiceCollection services, string dbConnectionString)
        where TRole : Role
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
