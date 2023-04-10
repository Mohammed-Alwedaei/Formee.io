using Application.Hubs;
using Domain.Interfaces;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Infrastructure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.Configure<NotificationsServiceBusConnection>(
            configuration.GetSection("ServiceBus"));

        services.AddScoped<IEmailsManager, EmailsManager>();
        services.AddScoped<INotificationsManager, NotificationsManager>();

        services.AddSingleton<NotificationsServiceBus>();

        services.AddHostedService<StartupService>();

        return services;
    }
}
