using Azure.Messaging.ServiceBus;
using System.Text;
using Newtonsoft.Json;
using ServiceBus.Messages;
using Microsoft.Extensions.Configuration;

namespace ServiceBus.ServiceBus;

public class HistoryServiceBus : IAzureServiceBus<HistoryMessage>
{
    private readonly IConfiguration _configuration;
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusSender _serviceBusSender;
    
    public HistoryServiceBus(IConfiguration configuration)    
    {
        _configuration = configuration;

        var clientOptions = new ServiceBusClientOptions
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };

        var connectionString = _configuration
            .GetValue<string>("ServiceBus:ConnectionString");

        var topic = _configuration
            .GetValue<string>("ServiceBus:History");

        _serviceBusClient = new ServiceBusClient(
            connectionString,
            clientOptions);

        _serviceBusSender = _serviceBusClient
        .CreateSender(topic);
    }

    public async Task SendMessage(HistoryMessage body)
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