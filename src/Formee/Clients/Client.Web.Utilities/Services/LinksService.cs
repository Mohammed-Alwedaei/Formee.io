using Client.Web.Utilities.Dtos.Links;
using Client.Web.Utilities.Models;
using System.Net.Http.Json;
using Client.Web.Utilities.Dtos;

namespace Client.Web.Utilities.Services;

public class LinksService
{
    public bool IsFetching;

    private readonly HttpClient _httpClient;

    public LinksService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LinkDto>> GetAllAsync(string containerId)
    {
        try
        {
            IsFetching = true;
            var url = $"/api/links/all/{containerId}";

            var response = await _httpClient
                .GetFromJsonAsync<List<LinkDto>>(url);

            IsFetching = false;

            return response ?? throw new Exception("something went wrong");
        }
        catch (Exception)
        {
            IsFetching = false;
            return new List<LinkDto>();
        }
    }

    public async Task<List<LinkHitDto>> GetLinkHitsById(int linkId)
    {
        try
        {
            IsFetching = true;

            var url = $"/api/links/redirects/all/{linkId}";

            var response = await _httpClient
                .GetFromJsonAsync<List<LinkHitDto>>(url);

            IsFetching = false;

            return response ?? throw new Exception("something went wrong"); ;
        }
        catch (Exception)
        {
            IsFetching = false;
            return new List<LinkHitDto>();
        }
    }

    public List<ChartModel> GenerateChartDataSeries(List<LinkHitDto> hits)
    {
        IsFetching = true;
        var dataSeries = new List<ChartModel>();

        foreach (var hit in hits)
        {
            var isAvailableDate = dataSeries
                .FirstOrDefault(c => c.Date.Date == hit.CreatedDate.Date);

            if (isAvailableDate is not null)
            {
                isAvailableDate.Count++;
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

        IsFetching = false;
        return dataSeries;
    }
}
