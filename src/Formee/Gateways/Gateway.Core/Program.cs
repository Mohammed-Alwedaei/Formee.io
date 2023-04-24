using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme, c =>
    {
        c.Authority = $"https://{builder.Configuration["Identity:Issuer"]}";
        c.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = builder.Configuration["Identity:Audience"],
            ValidIssuer = "dev-pnxnfhh8.us.auth0.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (builder.Configuration["Identity:SecretKey"]))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("cors");

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();

await app.UseOcelot();

app.Run();
