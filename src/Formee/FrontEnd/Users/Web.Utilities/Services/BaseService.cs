using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace Client.Web.Utilities.Services;

public class BaseService
{
    public IHttpClientFactory HttpClientFactory { get; set; }
    public IConfiguration Configuration { get; set; }

    public BaseService(IHttpClientFactory httpClientFactory, 
        IConfiguration configuration)
    {
        HttpClientFactory = httpClientFactory;
        Configuration = configuration;
    }

    public async Task<HttpClient> HttpClient()
    {
        var client = HttpClientFactory.CreateClient("ServerApi");

        var baseAddress = Configuration["GatewayUrl"];

        if (baseAddress != null)
        {
            client.BaseAddress = new Uri(baseAddress);
        }

        return client;
    }
}
