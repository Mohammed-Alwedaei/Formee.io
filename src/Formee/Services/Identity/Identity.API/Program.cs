using HealthChecks.UI.Client;
using Identity.API.Extensions;
using Identity.BusinessLogic.Contexts;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOpenApi()
    .AddApplicationDatabase(builder.Configuration)
    .AddConfiguration(builder.Configuration)
    .AddIdentityManagement(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("cors");

app.UseIdentityEndpoints();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var context = services.GetRequiredService<ApplicationDbContext>();

if (context.Database.GetPendingMigrations().Any())
{
    context.Database.Migrate();
}

app.MapHealthChecks("/healthcheck", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

