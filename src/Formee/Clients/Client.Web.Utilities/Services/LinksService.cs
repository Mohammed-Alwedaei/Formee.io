using Client.Web.Utilities.Dtos.Links;
using System.Net.Http.Json;

namespace Client.Web.Utilities.Services;

public class LinksService
{
    public List<LinkDto>? Links;

    public int LinksCount;

    public bool IsFetching;

    public bool IsSuccessFetch;

    public event Action OnChange;

    private readonly HttpClient _httpClient;

    public LinksService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetAllAsync(string containerId)
    {
        try
        {
            IsFetching = true;

            var url = $"/api/links/all/{containerId}";

            var response = await _httpClient
                .GetFromJsonAsync<List<LinkDto>>(url);

            Links = new List<LinkDto>();

            if (response != null)
            {
                Links = response;
                LinksCount = response.Count;
                IsSuccessFetch = true;
            }
            else
            {
                throw new Exception("something went wrong");
            }

            IsFetching = false;

            OnChange.Invoke();
        }
        catch (Exception)
        {
            Links = new List<LinkDto>();
            IsSuccessFetch = false;
        }
    }
}
