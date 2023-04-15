using Analytics.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddServiceEndpoints()
    .AddServiceConfiguration(builder.Configuration)
    .AddServicePersistant()
    .AddServiceDocumentation()
    .AddServiceDependencies()
    .AddServiceHealthChecks()
    .AddServiceSecurity();

var app = builder.Build();

app.UseDevelopmentEnvironment()
    .MapEndpointsAndUseSecurity()
    .UseProductionEnvironment();

app.Run();
