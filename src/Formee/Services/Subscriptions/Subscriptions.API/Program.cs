using Subscriptions.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddConfiguration(builder.Configuration)
    .AddEndpointsAndDocumentation()
    .AddServicesDependencies()
    .AddPersistent()
    .AddObservability()
    .AddIdentityAndSecurity();

var app = builder.Build();

app.UseTestingEnvironment()
    .UseProductionEnvironment()
    .MapEndpointsAndUseSecurity();

app.Run();