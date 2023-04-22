using Application.Hubs;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["Auth0:Authority"];
    options.Audience = builder.Configuration["Auth0:Audience"];
}); 

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("users", policy =>
    {
        policy.RequireClaim("user:read");
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("cors");

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationsHub>("/hubs/notifications");

//using var scope = app.Services.CreateScope();

//var services = scope.ServiceProvider;

//var context = services.GetRequiredService<ApplicationDbContext>();

//if (context.Database.GetPendingMigrations().Any())
//{
//    context.Database.Migrate();
//}

app.UseHealthMonitoring();

app.Run();
