using Links.API.Extensions;
using Links.API.Middlewares;
using Links.BusinessLogic.Contexts;
using Links.BusinessLogic.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<DbContext>();

builder.Services.AddScoped<ILinkRepository, LinkRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseLinksEndpoints()
    .UseRedirectLinksEndpoints();

app.Run();