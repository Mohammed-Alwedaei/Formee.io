using Microsoft.EntityFrameworkCore;
using Analytics.BusinessLogic.Mapper;
using Analytics.BusinessLogic.Contexts;
using Analytics.BusinessLogic.Repositories;
using ServiceBus.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(MappingConfiguration));

builder.Services.AddScoped<ISiteRepository, SiteRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPageHitRepository, PageHitRepository>();

builder.Services.AddAzureServiceBus();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapControllers();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

var context = services.GetRequiredService<ApplicationDbContext>();

if (context.Database.GetPendingMigrations().Any())
{
    context.Database.Migrate();
}

app.Run();
