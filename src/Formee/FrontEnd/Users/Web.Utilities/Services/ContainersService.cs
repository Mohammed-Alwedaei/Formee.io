using Client.Web.Utilities.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Client.Web.Utilities.Services;

public class ContainersService : BaseService
{
    private readonly AppStateService _appState;
    private readonly NavigationManager _navigationManager;

    public ContainersService(IHttpClientFactory httpClient, 
        AppStateService appState,
        IConfiguration configuration, 
        NavigationManager navigationManager) : base(httpClient, configuration)
    {
        _appState = appState;
        _navigationManager = navigationManager;
    }

    public async Task GetAllByUserIdAsync(Guid userId)
    {
        try
        {
            _appState.Containers.IsFetching = true;

            var url = $"/api/containers/all/{userId}";

            var client = await HttpClient();

            var response = await client.GetFromJsonAsync<List<ContainerDto>>(url);

            _appState.Containers.SetContainersCollectionState(response ?? new List<ContainerDto>());
        }
        catch (Exception)
        {
            _appState.Containers.SetContainersCollectionState(new List<ContainerDto>());
        }
        finally
        {
            _appState.Containers.IsFetching = false;
        }
    }

    public async Task GetByIdAsync(string containerId)
    {
        try
        {
            _appState.Containers.IsFetching = true;

            var url = $"/api/containers/{containerId}";

            var client = await HttpClient();

            var response = await client.GetFromJsonAsync<ContainerDto>(url);

            _appState.Containers.SetContainerState(response ?? new ContainerDto());
        }
        catch (Exception exception)
        {
            _appState.Containers.SetContainersCollectionState(new List<ContainerDto>());
        }
        finally
        {
            _appState.Containers.IsFetching = false;
        }
    }

    public async Task CreateAsync(ContainerDto? container)
    {
        try
        {
            const string url = "/api/containers";

            var client = await HttpClient();

            var response = await client.PostAsJsonAsync(url, container);

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new ForbiddenException("Access is not allowed");

            if (response.IsSuccessStatusCode)
            {
                var userId = _appState.Identity.User.Id;

                _navigationManager.NavigateTo($"/dashboard/containers?user_id={userId}");
            }
            else
            {
                throw new Exception();
            }
        }
        catch (ForbiddenException exception)
        {
            _navigationManager.NavigateTo("/dashboard/error?error_code=403&error_message=max_allowed_container");

            _appState.Containers.SetFeaturesState(true);
        }
        catch (Exception exception)
        {
            _navigationManager.NavigateTo($"/dashboard/error?error_code=500&error_message=something_wrong");
        }
        finally
        {
            _appState.Containers.IsFetching = false;
        }
    }

    public async Task UpdateAsync(ContainerDto? container)
    {
        try
        {
            const string url = "/api/containers/";

            var client = await HttpClient();

            var response = await client.PutAsJsonAsync(url, container);

            if (response.IsSuccessStatusCode)
            {
                var userId = _appState.Identity.User.Id;

                _navigationManager.NavigateTo($"/dashboard/containers?user_id={userId}");
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception exception)
        {
            _navigationManager.NavigateTo($"/dashboard/error?error_code=500&error_message=something_wrong");
        }
        finally
        {
            _appState.Containers.IsFetching = false;
        }
    }

    public async Task DeleteByIdAsync(string containerId)
    {
        try
        {
            var url = $"/api/containers/{containerId}";

            var client = await HttpClient();

            var response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var userId = _appState.Identity.User.Id;

                _navigationManager.NavigateTo($"/dashboard/containers?user_id={userId}");
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception exception)
        {
            _navigationManager.NavigateTo($"/dashboard/error?error_code=500&error_message=something_wrong");
        }
        finally
        {
            _appState.Containers.IsFetching = false;
        }
    }
}
