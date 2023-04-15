using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConfiguration(builder.Configuration)
    .AddDocumentation()
    .AddMonitoring()
    .AddPersistant()
    .AddIdentityAndSecurity();

var app = builder.Build();

app.UseDevelopmentEnvironment()
    .UseSecurityAndMapEndpoints();

app.Run();

