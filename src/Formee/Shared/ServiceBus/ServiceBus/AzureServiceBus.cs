using Azure.Messaging.ServiceBus;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ServiceBus.ServiceBus;

public abstract class AzureServiceBus<TImplementation> 
    : IAzureServiceBus<TImplementation> where TImplementation : class
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ServiceBusSender _serviceBusSender;
    private readonly ServiceBusProcessor _processor;

    protected AzureServiceBus(string connectionString, 
        string topic,   
        string subscription)    
    {
        var clientOptions = new ServiceBusClientOptions
        {
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };

        _serviceBusClient = new ServiceBusClient(
            connectionString,
            clientOptions);

        _serviceBusSender = _serviceBusClient
        .CreateSender(topic);

        _processor = _serviceBusClient.CreateProcessor(
            topic, 
            subscription, 
            new ServiceBusProcessorOptions());
    }

    public async Task SendMessage<TBody>(TBody body)
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

    protected abstract Task ProcessMessage(ProcessMessageEventArgs args);

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
}