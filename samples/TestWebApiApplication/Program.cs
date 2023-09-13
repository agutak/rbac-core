using AHutak.Rbac.Core.Abstractions.Entities.PermissionAggregate;
using AHutak.Rbac.Core.Abstractions.Entities.RoleAggregate;
using AHutak.Rbac.Core.Abstractions.Services;
using AHutak.Rbac.Core.Persistence.MsSql.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAdministrativeService<Role>, AdministrativeService<Role>>();
builder.Services.AddScoped<IReviewService<Role, SimplePermission>, ReviewService<Role, SimplePermission>>();
builder.Services.AddCoreRbacMsSqlPersistenceServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/weatherforecast", () =>
{
    
    return true;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

await app.RunAsync();
