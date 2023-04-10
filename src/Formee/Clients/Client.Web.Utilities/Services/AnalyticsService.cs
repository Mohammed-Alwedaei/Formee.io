using Client.Web.Utilities.Dtos.Analytics;
using Client.Web.Utilities.Models;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;

public class AnalyticsService
{
    public List<SiteDto> Sites;
    public List<PageHitDto> Hits;

    public List<ChartModel> HitChartDataSeries;

    public int HitsCount;

    public bool IsFetching;

    private readonly HttpClient _httpClient;

    public AnalyticsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

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

    public async Task<List<SiteDto>> GetAllSitesAsync(string containerId)
    {
        var url = $"/api/sites/all/{containerId}";

        var response = await _httpClient
            .GetFromJsonAsync<List<SiteDto>>(url);

        return response ?? new List<SiteDto>();
    }

    public async Task GetAllHitsInTimePeriodAsync
        (int siteId, DateTime startDate, DateTime endDate)
    {
        var formattedStartDate = startDate.ToString("yyyy-MM-dd");
        var formattedEndDate = endDate.ToString("yyyy-MM-dd");

        var url = $"/api/hits/all/{siteId}/{formattedStartDate}/{formattedEndDate}";

        var response = await _httpClient
            .GetFromJsonAsync<List<PageHitDto>>(url);

        if (response is not null)
        {
            Hits = new List<PageHitDto>();

            Hits = response;

            HitsCount = Hits.Count;
        }
    }

    public void GenerateChartDataSeries()
    {
        IsFetching = true;
        var dataSeries = new List<ChartModel>();
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
                isAvailableDate.Count = counter;
            }
            else
            {
                dataSeries.Add(new ChartModel
                {
                    Id = hit.Id,
                    Date = hit.CreatedDate,
                    Count = 1
                });
            }
        }

        HitChartDataSeries = new List<ChartModel>();
        HitChartDataSeries = dataSeries;

        IsFetching = false;
    }
}
