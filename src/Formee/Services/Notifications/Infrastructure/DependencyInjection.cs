using Domain.Interfaces;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBus.Extensions;
using ServiceBus.Models;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IEmailsManager, EmailsManager>();
        services.AddScoped<INotificationsManager, NotificationsManager>();

        services.AddConfiguration(configuration);

        services.AddScoped<NotificationRepository<NotificationModel>, NotificationsManager>();

        services
            .AddNotificationServiceBus()
            .AddHistoryServiceBus()
            .AddBackgroundProcessingTask();

        return services;
    }
}
