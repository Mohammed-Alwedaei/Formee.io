using API.Extensions;
using SynchronousCommunication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConfiguration(builder.Configuration)
    .AddDocumentation()
    .AddSyncCommunication()
    .AddMonitoring()
    .AddPersistant()
    .AddIdentityAndSecurity();

var app = builder.Build();

app.UseDevelopmentEnvironment()
    .UseSecurityAndMapEndpoints();

app.Run();

