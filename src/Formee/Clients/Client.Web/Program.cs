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

builder.Services.AddScoped<ContainersService>();

await builder.Build().RunAsync();
