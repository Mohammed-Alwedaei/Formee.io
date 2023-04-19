using Microsoft.Extensions.Logging;

namespace Client.Web.Utilities.Services;

public class AnalyticsService
{
    //Dependency injection
    private readonly HttpClient _httpClient;
    private readonly AppStateService _appState;
    private readonly ILogger<AnalyticsService> _logger;

    /// <summary>
    /// Initialize constructor and add configure dependency injection
    /// </summary>
    /// <param name="httpClient"></param>
    public AnalyticsService(HttpClient httpClient, AppStateService appState, ILogger<AnalyticsService> logger)
    {
        _httpClient = httpClient;
        _appState = appState;
        _logger = logger;
    }

    /// <summary>
    /// Get a single site by site id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task GetSiteByIdAsync(int id)
    {
        _appState.Analytics.IsFetching = true;
        
        var url = $"/api/sites/{id}";

        var response = await _httpClient
            .GetFromJsonAsync<SiteDto>(url);

        _appState.Analytics.Site = response ?? new SiteDto();
        
        _appState.Analytics.IsFetching = false;
    }

    /// <summary>
    /// Get all sites in a container by container id
    /// </summary>
    /// <param name="containerId"></param>
    /// <returns></returns>
    public async Task GetAllSitesAsync(string containerId)
    {
        _appState.Analytics.IsFetching = true;
        
        var url = $"/api/sites/all/{containerId}";

        var response = await _httpClient
            .GetFromJsonAsync<List<SiteDto>>(url);
        
        if (response is not null)
        {
            _appState.Analytics.Sites = new List<SiteDto>();  
            _appState.Analytics.Sites = response;
        }
        else
            _appState.Analytics.Sites = new List<SiteDto>();
        
        
        _appState.Analytics.IsFetching = false;
    }

    /// <summary>
    /// get all hits in a sites by site id in a period of time
    /// </summary>
    /// <param name="siteId"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="filter"></param>
    public async Task GetAllHitsInTimePeriodAsync
        (int siteId, DateTime startDate, DateTime endDate, string filter)
    {
        _appState.Analytics.IsFetching = true;
        
        var formattedStartDate = startDate.ToString("yyyy-MM-dd");
        var formattedEndDate = endDate.AddDays(1)
            .ToString("yyyy-MM-dd");

        var url = $"/api/hits/all/{siteId}/{formattedStartDate}/{formattedEndDate}?filter={filter}";

        var response = await _httpClient
            .GetFromJsonAsync<List<PageHitDto>>(url);

        if (response is not null)
        {
            _appState.Analytics.Hits = new List<PageHitDto>();
            _appState.Analytics.Hits = response;
        }
        else
            _appState.Analytics.Hits = new List<PageHitDto>();
        
        
        _appState.Analytics.IsFetching = false;
    }

    /// <summary>
    /// Get the top performing categories
    /// </summary>
    /// <param name="siteId"></param>
    public async Task GetTopPerformingCategories(int siteId)
    {
        var url = $"/api/category/top/{siteId}";

        _appState.Analytics.IsFetching = true;
        
        var response = await _httpClient.GetFromJsonAsync<List<BarChartModel>>(url);
        
        if (response is not null)
        {
            _appState.Analytics.TopPerformingCategories = new List<BarChartModel>();
            _appState.Analytics.TopPerformingCategories = response;
        }
        else
            _appState.Analytics.TopPerformingCategories = new List<BarChartModel>();

        _appState.Analytics.IsFetching = false;
    }
}
