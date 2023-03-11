using Identity.API.Extensions;

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

app.Run();

