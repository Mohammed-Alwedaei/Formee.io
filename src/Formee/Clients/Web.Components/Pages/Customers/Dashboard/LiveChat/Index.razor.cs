using Client.Web.Utilities.Dtos;
using Microsoft.AspNetCore.SignalR.Client;

namespace Clients.Web.Components.Pages.Customers.Dashboard.LiveChat;

[Route(Routes.LiveChat)]
public partial class Index
{
    private HubConnection HubConnection;

    private bool IsFetching { get; set; }

    private LiveChatDto LiveChat { get; set; }

    protected override async Task OnInitializedAsync()
    {
        LiveChat = new LiveChatDto
        {
            UserId = new Guid()
        };

        HubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7270/hubs/livechat")
            .Build();

        HubConnection.On<string, string>("ReceiveMessage",
            (user, message) =>
        {
            var encodedMsg = $"{user}: {message}";
            LiveChat.Messages.Add(encodedMsg);
            InvokeAsync(StateHasChanged);
        });

        await HubConnection.StartAsync();
    }

    private async Task SendMessage()
    {
        if (HubConnection is not null)
        {
            await HubConnection.SendAsync("SendMessage",
                LiveChat.UserId,
                LiveChat.CurrentMessage);
        }
    }
}

