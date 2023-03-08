using Microsoft.EntityFrameworkCore;
using Analytics.BusinessLogic.Mapper;
using Analytics.BusinessLogic.Contexts;
using Analytics.BusinessLogic.Repositories;
using Analytics.BusinessLogic.Repositories.IRepository;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
