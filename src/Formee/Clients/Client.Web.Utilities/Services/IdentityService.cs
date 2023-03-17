using Client.Web.Utilities.Dtos;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;
public class IdentityService
{
    private readonly HttpClient _httpClient;

    public IdentityService(HttpClient httpClient)
    {
        _httpClient = httpClient;


    }
    public async Task<UserDto?> CreateAsync(UserDto user)
    {
        var url = "/api/identity/user";

        var response = await _httpClient.PostAsJsonAsync(url, user);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        return new UserDto();
    }
}
