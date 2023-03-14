using Client.Web.Utilities.Dtos;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;

public class ContainersService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public ContainersService(HttpClient httpClient, 
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

        _httpClient.BaseAddress = new Uri(_configuration["GatewayUrl"]);
    }

    public async Task<List<ContainerDto>> GetAllByUserIdAsync(Guid userId)
    {
        var url = $"/api/containers/all/{userId}";

       var response = await _httpClient
           .GetFromJsonAsync<List<ContainerDto>>(url);

       if (response.Any())
       {
           return response;
       }

       return new List<ContainerDto>();
    }
}
