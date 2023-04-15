using Links.API.Middlewares;

namespace Links.API.Extensions;

public static class UseServicesExtension
{
    /// <summary>
    /// Use development environment to enable services in development environment
    /// </summary>
    /// <param name="app"></param>
    /// <returns>WebApplication</returns>
    public static WebApplication UseDevelopmentEnvironment(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();


        return app;
    }
    
    /// <summary>
    /// Map endpoints and use security (Authentication, Authorization, CORS)
    /// </summary>
    /// <param name="app"></param>
    /// <returns>WebApplication</returns>
    public static WebApplication MapEndpointsAndUseSecurity(this WebApplication app)
    {
        app.UseLinksEndpoints()
            .UseRedirectLinksEndpoints();

        app.UseAuthentication()
            .UseAuthorization();
        return app;
    }
}