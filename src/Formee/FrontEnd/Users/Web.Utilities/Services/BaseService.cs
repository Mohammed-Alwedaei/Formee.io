using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Client.Web.Utilities.Dtos.Identity;

namespace Client.Web.Utilities.Services;

public class BaseService
{
    public IHttpClientFactory HttpClientFactory { get; set; }
    public IConfiguration Configuration { get; set; }

    private readonly HttpClient _httpClient;

    public BaseService(IHttpClientFactory httpClientFactory, 
        IConfiguration configuration)
    {
        HttpClientFactory = httpClientFactory;
        Configuration = configuration;

        _httpClient = HttpClientFactory.CreateClient("ServerApi");

        var baseAddress = Configuration["Gateway"];

        if (baseAddress != null)
        {
            _httpClient.BaseAddress = new Uri(baseAddress);
        }
    }

    public async Task<HttpClient> HttpClient()
    {
        var accessToken = await GetAccessTokenAsync();

        _httpClient.DefaultRequestHeaders.Authorization = new
            AuthenticationHeaderValue(accessToken.TokenType, 
                accessToken.Token);

        return _httpClient;
    }

    private async Task<TokenDto?> GetAccessTokenAsync()
    {
        return await _httpClient
            .GetFromJsonAsync<TokenDto>("/gateway/identity/users/token");
    }
}
