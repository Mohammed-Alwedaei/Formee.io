using Infrastructure;
using Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandler>();

// Add services to the container.
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddOpenApi()
    .AddIdentityManagement(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
}

app.UseHttpsRedirection();

app.UseIdentityManagement();

app.UseFormsEndpoints()
    .UseFieldsEndpoints()
    .UseErrorEndpoints();

app.Run();