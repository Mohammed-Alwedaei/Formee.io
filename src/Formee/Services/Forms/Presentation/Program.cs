using Infrastructure;
using Presentation;
using Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandler>();

// Add services to the container.
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddOpenApi()
    .AddIdentityManagement(builder.Configuration)
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseIdentityManagement();

app.MapControllers();

app.Run();