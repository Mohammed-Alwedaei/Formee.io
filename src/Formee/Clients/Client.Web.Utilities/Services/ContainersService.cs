namespace Client.Web.Utilities.Services;

public class ContainersService
{
    private readonly HttpClient _httpClient;
    private readonly AppStateService _appState; 

    public ContainersService(HttpClient httpClient, AppStateService appState)
    {
        _httpClient = httpClient;
        _appState = appState;
    }

    public async Task GetAllByUserIdAsync(Guid userId)
    {
        _appState.Containers.IsFetching = true;
        
        var url = $"/api/containers/all/{userId}";

        var response = await _httpClient
            .GetFromJsonAsync<List<ContainerDto>>(url);
        
        _appState.Containers.SetContainersCollectionState(response ?? new List<ContainerDto>());

        _appState.Containers.IsFetching = false;
    }

    public async Task GetByIdAsync(string containerId)
    {
        _appState.Containers.IsFetching = true;
        
        var url = $"/api/containers/{containerId}";

        var response = await _httpClient
            .GetFromJsonAsync<ContainerDto>(url);

        _appState.Containers.SetContainerState(response ?? new ContainerDto());
        
        _appState.Containers.IsFetching = false;
    }

    public async Task<ContainerDto?> CreateAsync(ContainerDto? container)
    {
        var url = "/api/containers";

        var response = await _httpClient
            .PostAsJsonAsync(url, container);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ContainerDto?>();
        }

        return new ContainerDto();
    }

    public async Task<ContainerDto?> UpdateAsync(ContainerDto? container)
    {
        var url = "/api/containers/";

        var response = await _httpClient
            .PutAsJsonAsync(url, container);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ContainerDto?>();
        }

        return new ContainerDto();
    }

    public async Task<ContainerDto?> DeleteByIdAsync(string containerId)
    {
        var url = $"/api/containers/{containerId}";

        var response = await _httpClient
            .DeleteAsync(url);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ContainerDto?>();
        }

        return new ContainerDto();
    }
}
