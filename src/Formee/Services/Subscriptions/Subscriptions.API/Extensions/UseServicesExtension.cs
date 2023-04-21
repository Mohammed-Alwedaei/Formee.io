using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Subscriptions.BusinessLogic.DbContexts;

namespace Subscriptions.API.Extensions;

public static class UseServicesExtension
{
    public static WebApplication MapEndpointsAndUseSecurity(this WebApplication app)
    {
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.MapHealthChecks("/healthcheck", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }

    public static WebApplication UseTestingEnvironment(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return app;
        }

        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    public static WebApplication UseProductionEnvironment(this WebApplication app)
    {
        if (!app.Configuration.GetValue<bool>("MigrateDatabase"))
        {
            return app;
        }

        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        return app;
    }
}