namespace API.Extensions;

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
    /// Map endpoints and use security (Authentication, Authorization, CORS)
    /// </summary>
    /// <param name="app"></param>
    /// <returns>WebApplication</returns>
    public static WebApplication UseSecurityAndMapEndpoints(this WebApplication app)
    {
        if (app.Configuration.GetValue<bool>("Settings:EnableHttpsRedirection"))
        {
            app.UseHttpsRedirection();
        }
        
        app.UseCors("cors");

        app.UseAuthentication()
            .UseAuthorization();
            
        app.UseContainerEndpoints()
            .MapHealthChecks("/hc");

        return app;
    }
}