using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ServiceBus.Extensions;
using ServiceBus.Models;
using System.Text;

namespace ServiceBus.ServiceBus;

public class NotificationsServiceBus : AzureServiceBus<NotificationsServiceBus>
{
    private readonly NotificationRepository<NotificationModel> _notificationServiceBus;

    public NotificationsServiceBus(
        NotificationRepository<NotificationModel> notificationServiceBus, 
        IOptions<NotificationsServiceBusConnection> connection) 
        : base(connection.Value.ConnectionString, connection.Value.Topic, connection.Value.Subscription)
    {
        _notificationServiceBus = notificationServiceBus;
    }

    protected override async Task ProcessMessage(ProcessMessageEventArgs args)
    {
        var message = args.Message;

        var body = Encoding.UTF8.GetString(message.Body);

        var model = JsonConvert.DeserializeObject<CustomServiceBusMessage<NotificationModel>>(body);

        await _notificationServiceBus.CreateAndSendAsync<NotificationModel>(model.Entity);
    }
}
