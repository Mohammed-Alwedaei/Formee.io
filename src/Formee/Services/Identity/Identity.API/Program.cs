using Identity.API.Extensions;
using Identity.BusinessLogic.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOpenApi()
    .AddApplicationDatabase(builder.Configuration)
    .AddConfiguration(builder.Configuration)
    .AddIdentityManagement(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseIdentityEndpoints();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var context = services.GetRequiredService<ApplicationDbContext>();

if (context.Database.GetPendingMigrations().Any())
{
    context.Database.Migrate();
}


app.Run();

