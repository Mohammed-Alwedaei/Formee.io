using Azure.Messaging.ServiceBus;
using System.Text;
using Newtonsoft.Json;
using ServiceBus.Messages;
using Microsoft.Extensions.Configuration;
using ServiceBus.Models;

namespace ServiceBus.ServiceBus;

public class NotificationServiceBus : IAzureServiceBus<NotificationMessage>
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusSender _serviceBusSender;
    
    public NotificationServiceBus(IConfiguration configuration)    
    {
        var clientOptions = new ServiceBusClientOptions
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };

        var connectionString = configuration.GetValue<string>("ServiceBus:ConnectionString");

        var topic = configuration.GetValue<string>("ServiceBus:NotificationsTopic");

        _serviceBusClient = new ServiceBusClient(connectionString, clientOptions);

        _serviceBusSender = _serviceBusClient
        .CreateSender(topic);
    }

    public async Task SendMessage(NotificationMessage body)
    {
        var message = JsonConvert.SerializeObject(body);

        var messageToSend = new ServiceBusMessage
            (Encoding.UTF8.GetBytes(message));

        try
        {
            await _serviceBusSender.SendMessageAsync(messageToSend);
        }
        finally
        {
            await _serviceBusClient.DisposeAsync();
            await _serviceBusSender.DisposeAsync();
        }
    }
}