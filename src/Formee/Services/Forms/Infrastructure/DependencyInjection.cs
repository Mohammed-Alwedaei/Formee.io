using Application;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
       this IServiceCollection services, IConfiguration config)
    {
        services.AddApplication();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IGenericRepository<FormEntity>, 
            GenericRepository<FormEntity>>();

        services.AddScoped<IGenericRepository<FieldEntity>,
            GenericRepository<FieldEntity>>();

        services.AddScoped<IGenericRepository<FormResponseEntity>,
            GenericRepository<FormResponseEntity>>();

        return services;
    }
}
