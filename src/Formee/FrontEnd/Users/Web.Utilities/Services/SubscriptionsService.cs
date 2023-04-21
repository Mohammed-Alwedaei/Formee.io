namespace Client.Web.Utilities.Services;

public class SubscriptionsService
{
    private HttpClient _httpClient;

    public SubscriptionsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateSubscriptionsAsync()
    {

    }
}
