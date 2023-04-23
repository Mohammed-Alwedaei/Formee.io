using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Polly;
using SynchronousCommunication.Dtos;

namespace SynchronousCommunication.HttpClients;

public class SubscriptionsClient : ISubscriptionsClient
{
    private readonly HttpClient _httpClient;

    private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy =
        Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(x =>
                x.StatusCode >= HttpStatusCode.InternalServerError || x.StatusCode == HttpStatusCode.RequestTimeout)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds((Math.Pow(2, retryAttempt))));

    public SubscriptionsClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(configuration.GetValue<string>("Services:Subscriptions") ?? string.Empty);
    }
    
    public async Task<UserSubscriptionDto> GetSubscriptionFeaturesAsync(Guid userId)
    {
        var url = $"/api/users/subscription/{userId}";
        
        var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync(url));

        if (response is null)
            return new UserSubscriptionDto();

        var content = await response.Content.ReadAsStringAsync();

        var userSubscription = JsonConvert.DeserializeObject<UserSubscriptionDto>(content);

        return userSubscription;
    }

    public async Task<SubscriptionDto> GetDefaultSubscription()
    {
        const string url = "/api/subscriptions/default";

        var response = await _retryPolicy.ExecuteAsync(() => _httpClient.GetAsync(url));

        if (response is null)
            return new SubscriptionDto();

        var content = await response.Content.ReadAsStringAsync();

        var subscription = JsonConvert.DeserializeObject<SubscriptionDto>(content);

        return subscription;
    }

    public async Task<UserSubscriptionDto?> CreateUserAndAssignSubscriptionAsync(Guid userId, string email)
    {
        const string createUserUrl = "/api/users";

        var userToCreate = new UserDto
        {
            GlobalUserId = userId,
            Email =email,
            Role = "User"
        };

        var createUserResponse = await _retryPolicy.ExecuteAsync(() =>
            _httpClient.PostAsJsonAsync(createUserUrl, userToCreate));

        if(!createUserResponse.IsSuccessStatusCode)
            return new UserSubscriptionDto();

        var createdUser = await createUserResponse.Content
            .ReadFromJsonAsync<UserDto>();

        var defaultSubscription = await GetDefaultSubscription();

        const string assignSubscriptionUrl = "/api/subscriptions/users";

        var userToUpdate = new UpdateUserSubscriptionDto
        {
            UserId = createdUser.Id,
            SubscriptionId = defaultSubscription.Id
        };

        var content = new StringContent(JsonConvert.SerializeObject(userToUpdate), 
            Encoding.UTF8, 
            "application/json");

        var subscription = await _retryPolicy.ExecuteAsync(() => 
            _httpClient.PutAsync(assignSubscriptionUrl, content));

        return await subscription.Content.ReadFromJsonAsync<UserSubscriptionDto>();
    }
}