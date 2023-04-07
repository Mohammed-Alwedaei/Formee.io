using API.Entities;
using API.Extensions;
using API.Services;
using ServiceBus.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ContainersDatabaseConfiguration>(
    builder.Configuration.GetSection("ContainersDatabase"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ContainersService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services
    .AddConfiguration(builder.Configuration)
    .AddNotificationServiceBus()
    .AddHistoryServiceBus()
    .AddBackgroundProcessingTask();


builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("cors");

app.UseContainerEndpoints();

app.Run();

