using Links.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConfiguration(builder.Configuration)
    .AddExceptionHandling()
    .AddDocumentation()
    .AddPersistant()
    .AddHealthMonitoring();

var app = builder.Build();

app.UseDevelopmentEnvironment()
    .MapEndpointsAndUseSecurity();

app.Run();