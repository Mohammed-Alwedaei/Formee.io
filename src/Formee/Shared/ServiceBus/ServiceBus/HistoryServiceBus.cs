using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using ServiceBus.Extensions;
using ServiceBus.Models;

namespace ServiceBus.ServiceBus;

public class HistoryServiceBus : AzureServiceBus<HistoryServiceBus>
{
    public HistoryServiceBus(
        IOptions<HistoryServiceBusConnection> connection)
        : base(connection.Value.ConnectionString, 
            connection.Value.Topic, 
            connection.Value.Subscription)
    {
        
    }

    protected override async Task ProcessMessage(ProcessMessageEventArgs args)
    {
        throw new NotImplementedException();
    }
}
