using Client.Web.Utilities.Dtos;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;

public class ContainersService
{
    public List<ContainerDto> Containers;

    public int ContainersCount; 

    public bool IsFetching;

    public event Action OnChange;

    private readonly HttpClient _httpClient;

    public ContainersService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetAllByUserIdAsync(Guid userId)
    {
        IsFetching = true;

        var url = $"/api/containers/all/{userId}";

        var response = await _httpClient
            .GetFromJsonAsync<List<ContainerDto>>(url);

        if (response.Any())
        {
            Containers = new List<ContainerDto>();

            Containers = response;

            ContainersCount = Containers.Count;
        }

        IsFetching = false;

        OnChange.Invoke();
    }

    public async Task<ContainerDto> GetByIdAsync(string containerId)
    {
        var url = $"/api/containers/{containerId}";

        var response = await _httpClient
            .GetFromJsonAsync<ContainerDto>(url);

        if (response is not null)
        {
            return response;
        }

        return new ContainerDto();
    }

    public async Task<bool> CreateAsync(ContainerDto container)
    {
        var url = "/api/containers";

        var response = await _httpClient
            .PostAsJsonAsync(url, container);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> UpdateAsync(ContainerDto container)
    {
        var url = "/api/containers/";

        var response = await _httpClient
            .PutAsJsonAsync(url, container);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteByIdAsync(string containerId)
    {
        var url = $"/api/containers/{containerId}";

        var response = await _httpClient
            .DeleteAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        return false;
    }
}
