using System.Text;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

builder.Services.AddHealthChecksUI()
    .AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/hc", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json; charset=utf-8";
        var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(report));
        await context.Response.Body.WriteAsync(bytes);
    }
});

app.UseHealthChecksUI(config => config.UIPath =  "/hc-ui");

app.Run();