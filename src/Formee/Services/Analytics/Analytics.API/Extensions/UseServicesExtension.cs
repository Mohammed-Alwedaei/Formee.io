using Analytics.BusinessLogic.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Analytics.API.Extensions;

public static class UseServicesExtension
{
    /// <summary>
    /// Use development environment to enable services in development environment
    /// </summary>
    /// <param name="app"></param>
    /// <returns>WebApplication</returns>
    public static WebApplication UseDevelopmentEnvironment(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
    
    /// <summary>
    /// Use and migrate pending migrations (Update Database Schema)
    /// </summary>
    /// <param name="app"></param>
    /// <returns>WebApplication</returns>
    public static WebApplication UseProductionEnvironment(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<ApplicationDbContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        return app;
    }

    /// <summary>
    /// Map endpoints and use security (Authentication, Authorization, CORS)
    /// </summary>
    /// <param name="app"></param>
    /// <returns>WebApplication</returns>
    public static WebApplication MapEndpointsAndUseSecurity(this WebApplication app)
    {
        if (app.Configuration.GetValue<bool>("Settings:EnableHttpsRedirection"))
        {
            app.UseHttpsRedirection();
        }
        
        app.UseCors("cors");
        
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}