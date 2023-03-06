using API.Entities;
using API.Extensions;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ContainersDatabaseConfiguration>(
    builder.Configuration.GetSection("ContainersDatabase"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<ContainersService>();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseContainerEndpoints();

app.Run();

