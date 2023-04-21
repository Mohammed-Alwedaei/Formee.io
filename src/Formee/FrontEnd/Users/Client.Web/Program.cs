using Client.Web;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSyncfusionBlazor();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
    builder.Configuration.GetValue<string>("SyncfusionLicenseKey")!);

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration
        .GetValue<string>("GatewayUrl")!)
});

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
});

builder.Services.AddScoped<ContainersService>();
builder.Services.AddScoped<IdentityService>();
builder.Services.AddScoped<AnalyticsService>();
builder.Services.AddScoped<FormsService>();
builder.Services.AddScoped<HistoryService>();
builder.Services.AddScoped<LinksService>();
builder.Services.AddScoped<NotificationsService>();

builder.Services.AddSingleton<AppStateService>();

await builder.Build().RunAsync();