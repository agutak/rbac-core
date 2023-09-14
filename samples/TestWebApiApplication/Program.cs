using AHutak.Rbac.Core.Abstractions.Entities;
using AHutak.Rbac.Core.Abstractions.Services;
using AHutak.Rbac.Core.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RBAC ->
builder.Services.AddScoped<IAdministrativeService<Role>, AdministrativeService>();
builder.Services.AddScoped<IReviewService<Role, SimplePermission>, ReviewService>();

var dbConnectionString = builder.Configuration.GetConnectionString("RbacDb");

builder.Services.AddDbContext<RbacDbContext>(builder =>
    builder.UseSqlServer(
        dbConnectionString,
        options => options
            .EnableRetryOnFailure()
            .MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));

builder.Services.AddScoped<IRolesRepository<Role>, RolesRepository>();
// <- RBAC

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/test", async (
    IAdministrativeService<Role> administrativeService,
    IReviewService<Role, SimplePermission> reviewService,
    CancellationToken cancellationToken) =>
{
    var role = new Role(Guid.NewGuid(), "MyFirstRole", null);

    var userId = Guid.NewGuid().ToString();
    var permId = Guid.NewGuid();
    var roleId = role.Id;

    role.AssignUser(userId);
    role.GrantPermission(permId);

    await administrativeService.AddRoleAsync(role, cancellationToken);

    //await administrativeService.DeassignUserAsync(userId, roleId, cancellationToken);

    //await administrativeService.RevokePermissionAsync(permId, roleId, cancellationToken);

    //await administrativeService.AssignUserAsync(userId, roleId, cancellationToken);

    //await administrativeService.GrantPermissionAsync(permId, roleId, cancellationToken);

    //var result = await reviewService.GetAssignedRolesAsync(userId, cancellationToken);

    //var result = await reviewService.GetAssignedUsersAsync(roleId, cancellationToken);

    //var result = await reviewService.GetRolePermissionsAsync(roleId, cancellationToken);

    //var result = await reviewService.GetUserPermissionsAsync(userId, cancellationToken);

    //var result = await administrativeService.DeleteRoleAsync(roleId, cancellationToken);

    return true;//result;
})
.WithName("TestRoles")
.WithOpenApi();

await app.RunAsync();
