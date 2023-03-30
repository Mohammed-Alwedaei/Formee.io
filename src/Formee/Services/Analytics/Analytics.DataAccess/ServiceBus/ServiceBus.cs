using Azure.Core;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Analytics.BusinessLogic.ServiceBus;

public class ServiceBus : IServiceBus
{
    private readonly IConfiguration _configuration;
    protected ServiceBusProcessor ServiceBusProcessor;
    protected ServiceBusClient ServiceBusClient;

    public ServiceBus(IConfiguration configuration)
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

        ServiceBusProcessor = ServiceBusClient.CreateProcessor(
            "topic-history",
            "formeeaAnalytics");
    }

    private async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string body = args.Message.Body.ToString();

        await args.CompleteMessageAsync(args.Message);
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());

        return Task.CompletedTask;
    }

    public async Task Start()
    {
        ServiceBusProcessor.ProcessMessageAsync += MessageHandler;
        ServiceBusProcessor.ProcessErrorAsync += ErrorHandler;

        await ServiceBusProcessor.StartProcessingAsync();
    }

    public async Task Stop()
    {
        ServiceBusProcessor.ProcessMessageAsync -= MessageHandler;
        ServiceBusProcessor.ProcessErrorAsync -= ErrorHandler;

        await ServiceBusClient.DisposeAsync();
    }
}
