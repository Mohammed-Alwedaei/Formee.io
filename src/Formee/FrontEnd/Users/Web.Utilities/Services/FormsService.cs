using Client.Web.Utilities.Dtos.Forms;
using Microsoft.Extensions.Configuration;

namespace Client.Web.Utilities.Services;

public class FormsService : BaseService
{
    private readonly AppStateService _appState;
    public FormsService(IHttpClientFactory httpClient,
        AppStateService appState,
        IConfiguration configuration) : base(httpClient, configuration, appState)
    {
        _appState = appState;
    }

    public async Task<List<FormDto>> GetAllBySiteIdAsync(int siteId)
    {
        var url = $"/api/forms/all/{siteId}";

        var client = await HttpClient();

        var response = await client
            .GetFromJsonAsync<ResponseModel<List<FormDto>>>(url);

        return response is null ? new List<FormDto>() : response.Results;
    }

    public async Task<List<FormResponseDto>> GetAllResponsesByFormIdAsync
        (int formId)
    {
        var url = $"/api/formresponse/all/{formId}";

        var client = await HttpClient();

        var response = await client
            .GetFromJsonAsync<ResponseModel<List<FormResponseDto>>>(url);

        return response is null ? new List<FormResponseDto>() : response.Results;
    }
}
