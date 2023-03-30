using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text;
using Newtonsoft.Json;

namespace ServiceBus.ServiceBus;

public class AzureServiceBus : IAzureServiceBus
{
    private readonly IConfiguration _configuration;
    protected ServiceBusClient ServiceBusClient;
    protected ServiceBusSender ServiceBusSender;

    public AzureServiceBus(IConfiguration configuration)
    {
        _configuration = configuration;

        var connectionString = _configuration
            .GetConnectionString("ServiceBus");

        var clientOptions = new ServiceBusClientOptions
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };

        ServiceBusClient = new ServiceBusClient(
            connectionString,
            clientOptions);

        ServiceBusSender = ServiceBusClient
            .CreateSender("topic-history");
    }

    public async Task SendMessage<TBody>(TBody body)
    {
        var message = JsonConvert.SerializeObject(body);

        var messageToSend = new ServiceBusMessage
            (Encoding.UTF8.GetBytes(message));

        try
        {
            await ServiceBusSender.SendMessageAsync(messageToSend);
        }
        finally
        {
            await ServiceBusClient.DisposeAsync();
            await ServiceBusSender.DisposeAsync();
        }
    }
}