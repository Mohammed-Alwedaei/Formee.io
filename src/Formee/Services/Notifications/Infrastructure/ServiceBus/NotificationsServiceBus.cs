using Azure.Messaging.ServiceBus;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceBus.Messages;

namespace Infrastructure.ServiceBus;

public class NotificationsServiceBus
{
    private readonly INotificationsManager _notificationsManager;
    private readonly ServiceBusProcessor _processor;

    public NotificationsServiceBus(
        IOptions<NotificationsServiceBusConnection> connection, 
        IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();

        _notificationsManager = scope.ServiceProvider
            .GetRequiredService<INotificationsManager>();

        var clientOptions = new ServiceBusClientOptions
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };

        var serviceBusClient = new ServiceBusClient(
            connection.Value.ConnectionString,
            clientOptions);

        _processor = serviceBusClient.CreateProcessor(
            connection.Value.NotificationsTopic,
            connection.Value.NotificationsSubscription,
            new ServiceBusProcessorOptions());
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }

    public async Task StartProcessing()
    {
        _processor.ProcessMessageAsync += ProcessMessage;

        _processor.ProcessErrorAsync += ErrorHandler;

        await _processor.StartProcessingAsync();
    }

    public async Task StopProcessing()
    {
        await _processor.StopProcessingAsync();
    }

    public async Task ProcessMessage(ProcessMessageEventArgs args)
    {
        var message = args.Message;

        var body = message.Body.ToString();

        var settings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        var notificationMessage = JsonConvert
            .DeserializeObject<NotificationMessage>(body, settings);

        var notification = new NotificationEntity
        {
            GlobalUserId = notificationMessage.Entity.GlobalUserId,
            Heading = notificationMessage.Entity.Heading,
            Message = notificationMessage.Entity.Message,
            IsRead = notificationMessage.Entity.IsRead,
            CreatedDate = DateTime.Now
        };

        await _notificationsManager.CreateAndSendAsync(notification);

        await args.CompleteMessageAsync(message);
    }
}
