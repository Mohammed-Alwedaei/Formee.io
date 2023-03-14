using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", policy =>
    {
        policy.WithOrigins("https://localhost:7255", "http://localhost:5123")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("cors");

await app.UseOcelot();

app.Run();
