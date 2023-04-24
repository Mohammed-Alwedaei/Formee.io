using Client.Web.Utilities.Dtos.Links;
using Client.Web.Utilities.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Client.Web.Utilities.Services;

public class LinksService : BaseService
{
    private readonly AppStateService _appState;
    private readonly NavigationManager _navigationManager;
    private readonly ILogger<LinksService> _logger;

    public LinksService(IHttpClientFactory httpClient,
        AppStateService appState,
        IConfiguration configuration, 
        NavigationManager navigationManager,
        ILogger<LinksService> logger) 
        : base(httpClient, configuration, appState)
    {
        _appState = appState;
        _navigationManager = navigationManager;
        _logger = logger;
    }

    public async Task GetAllAsync(string? containerId)
    {
        try
        {
            _appState.Links.InvertFetchingState();

            var url = $"/gateway/links/all/{containerId}";

            var client = await HttpClient();

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var links = await response.Content.ReadFromJsonAsync<List<LinkDto>>();
                _appState.Links.SetLinksCollectionState(links);

                _appState.Links.InvertFetchingState();
            }

            if((int)response.StatusCode == 404)
            {
                _appState.Links.SetLinksCollectionState(new List<LinkDto>());
                throw new NotFoundException("the requested resource is not available");
            }
            
        }
        catch (Exception)
        {
            _logger.LogInformation("No Links are found");
        }
    }
    
    public async Task GetLinkById(int linkId)
    {
        try
        {
            _appState.Links.InvertFetchingState();

            var url = $"/gateway/links/{linkId}";

            var client = await HttpClient();

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var link = await response.Content.ReadFromJsonAsync<LinkDto>();
                _appState.Links.SetLinkState(link);

                _appState.Links.InvertFetchingState();
            }

            if((int)response.StatusCode == 404)
            {
                _appState.Links.SetLinkState(new LinkDto());
                throw new NotFoundException("the requested resource is not available");
            }
            
        }
        catch (Exception)
        {
            _logger.LogInformation("No Links are found");
        }
    }

    public async Task GetLinkHitsById(int linkId)
    {
        _appState.Links.InvertFetchingState();

        var url = $"/gateway/links/hits/all/{linkId}";

        var client = await HttpClient();

        var response = await client.GetFromJsonAsync<List<DateChartModel>>(url);

        _appState.Links.SetLinkHitsState(response ?? new List<DateChartModel>());
        
        _appState.Links.InvertFetchingState();
    }

    public async Task GetAllHitsByContainerId(string? containerId, DateTime startDate, DateTime endDate)
    {
        var formattedStartDate = startDate.ToString("yyyy-MM-dd");
        var formattedEndDate = endDate.AddDays(1)
            .ToString("yyyy-MM-dd");

        var url = $"/gateway/links/hits/all/{containerId}/{formattedStartDate}/{formattedEndDate}";

        var client = await HttpClient();

        var response = await client.GetFromJsonAsync<List<DateChartModel>>(url);

        _appState.Links.SetLinkHitsInContainerCollectionState(response ?? new List<DateChartModel>());
    }

    public async Task CreateAsync(LinkDto link)
    {
        try
        {
            const string url = "/gateway/links";

            var client = await HttpClient();

            var response = await client.PostAsJsonAsync(url, link);

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new ForbiddenException("Access is not allowed");

            if (response.IsSuccessStatusCode)
            {
                var userId = _appState.Identity.User.Id;

                _navigationManager.NavigateTo($"/dashboard/links?user_id={userId}&container_id={link.ContainerId}");
            }
            else
            {
                throw new Exception();
            }
        }
        catch (ForbiddenException exception)
        {
            _navigationManager.NavigateTo("/dashboard/error?error_code=403&error_message=max_allowed_links");

            _appState.Containers.SetFeaturesState(true);
        }
        catch (Exception exception)
        {
            _navigationManager.NavigateTo("/dashboard/error?error_code=500&error_message=something_wrong");
        }
        finally
        {
            _appState.Containers.IsFetching = false;
        }
    }
    
    public async Task DeleteAsync(int id)
    {
        try
        {
            var url = $"/gateway/links/{id}";

            var client = await HttpClient();

            var response = await client.DeleteAsync(url);

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new ForbiddenException("Access is not allowed");

            if (response.IsSuccessStatusCode)
            {
                var userId = _appState.Identity.User.Id;

                _navigationManager.NavigateTo($"/dashboard/links?user_id={userId}");
            }
            else
            {
                throw new Exception();
            }
        }
        catch (ForbiddenException)
        {
            _navigationManager.NavigateTo("/dashboard/error?error_code=403&error_message=max_allowed_links");

            _appState.Containers.SetFeaturesState(true);
        }
        catch (Exception)
        {
            _navigationManager.NavigateTo("/dashboard/error?error_code=500&error_message=something_wrong");
        }
        finally
        {
            _appState.Containers.IsFetching = false;
        }
    }
}
