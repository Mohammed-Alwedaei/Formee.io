using Client.Web.Utilities.Dtos.Forms;
using Client.Web.Utilities.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;


namespace Client.Web.Utilities.Services;

public class FormsService
{
    private readonly HttpClient _httpClient;

    public FormsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<FormDto>> GetAllBySiteIdAsync(int siteId)
    {
        var url = $"/api/forms/all/{siteId}";

        var response = await _httpClient
            .GetFromJsonAsync<ResponseModel<List<FormDto>>>(url);

        return response is null ? new List<FormDto>() : response.Results;
    }
}
