using Client.Web.Utilities.Dtos.Analytics;
using Client.Web.Utilities.Models;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;

public class AnalyticsService
{
    //Raw data collections
    public List<SiteDto> Sites;
    public List<PageHitDto> Hits;

    //Display collection
    public List<DateChartModel> HitChartDataSeries;
    public List<BarChartModel> TopPerformingCategories;

    //Collection meta
    public int HitsCount;
    public bool IsFetching;

    //Dependency injection
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initialize constructor and add configure dependency injection
    /// </summary>
    /// <param name="httpClient"></param>
    public AnalyticsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Get a single site by site id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<SiteDto> GetSiteByIdAsync(int id)
    {
        var url = $"/api/sites/{id}";

        var response = await _httpClient
            .GetFromJsonAsync<SiteDto>(url);

        if (response is not null)
        {
            return response;
        }

        return new SiteDto();
    }

    /// <summary>
    /// Get all sites in a container by container id
    /// </summary>
    /// <param name="containerId"></param>
    /// <returns></returns>
    public async Task<List<SiteDto>> GetAllSitesAsync(string containerId)
    {
        var url = $"/api/sites/all/{containerId}";

        var response = await _httpClient
            .GetFromJsonAsync<List<SiteDto>>(url);

        return response ?? new List<SiteDto>();
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
        var formattedStartDate = startDate.ToString("yyyy-MM-dd");
        var formattedEndDate = endDate.AddDays(1)
            .ToString("yyyy-MM-dd");

        var url = $"/api/hits/all/{siteId}/{formattedStartDate}/{formattedEndDate}?filter={filter}";

        var response = await _httpClient
            .GetFromJsonAsync<List<PageHitDto>>(url);

        if (response is not null)
        {
            Hits = new List<PageHitDto>();

            Hits = response;

            HitsCount = Hits.Count;
        }
    }

    /// <summary>
    /// Generate datetime chart type for hits 
    /// </summary>
    public void GenerateChartDataSeries()
    {
        IsFetching = true;
        var dataSeries = new List<DateChartModel>();
        var counter = 0;

        foreach (var hit in Hits)
        {
            counter += 1;

            var isAvailableDate = dataSeries
                .FirstOrDefault(c => c.Date.Date
                                     == hit.CreatedDate.Date 
                                     && c.Date.Hour - hit.CreatedDate.Hour == 0);

            if (isAvailableDate is not null)
            {
                isAvailableDate.Count++;
            }
            else
            {
                dataSeries.Add(new DateChartModel
                {
                    Id = hit.Id,
                    Date = hit.CreatedDate,
                    Count = 1
                });
            }
        }

        HitChartDataSeries = new List<DateChartModel>();
        HitChartDataSeries = dataSeries;

        IsFetching = false;
    }

    /// <summary>
    /// Generate top performing categories of a site 
    /// </summary>
    public void GenerateTopPerformingCategories()
    {
        IsFetching = true;

        var dataSeries = new List<BarChartModel>();
        var counter = 0;

        foreach (var hit in Hits)
        {
            counter += 1;

            var hasCategory = dataSeries
                .FirstOrDefault(c => c.Name == hit.Category.Name);

            if (hasCategory is not null)
            {
                hasCategory.Count++;
            }
            else
            {
                dataSeries.Add(new BarChartModel
                {
                    Name = hit.Category.Name,
                    Count = 1
                });
            }
        }

        TopPerformingCategories = new List<BarChartModel>();
        TopPerformingCategories = dataSeries;

        IsFetching = false;
    }
}
