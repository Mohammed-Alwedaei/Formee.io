using Microsoft.Extensions.Configuration;

namespace Client.Web.Utilities.Services;

public class SubscriptionsService : BaseService
{
    private readonly AppStateService _appState;

    public SubscriptionsService(IHttpClientFactory httpClient,
        AppStateService appState,
        IConfiguration configuration) : base(httpClient, configuration)
    {
        _appState = appState;
    }

    public async Task CreateSubscriptionsAsync()
    {

    }
}
