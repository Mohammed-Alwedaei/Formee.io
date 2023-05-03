using Client.Web.Utilities.Dtos.Forms;
using Client.Web.Utilities.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Client.Web.Utilities.Services;

public class FormsService : BaseService
{
    private readonly ILogger<FormsService> _logger;
    private readonly NavigationManager _navigationManager;

    public FormsService(IHttpClientFactory httpClient,
        AppStateService appState,
        IConfiguration configuration, 
        ILogger<FormsService> logger, 
        NavigationManager navigationManager) 
        : base(httpClient, configuration, appState)
    {
        _logger = logger;
        _navigationManager = navigationManager;
    }

    /// <summary>
    /// Fetch all forms in a site and mutate the old state of forms collection
    /// </summary>
    /// <param name="siteId"></param>
    /// <returns></returns>
    public async Task GetAllBySiteIdAsync(int siteId)
    {
        try
        {
            AppState.Forms.InverseFetchingState();

            var url = $"/gateway/forms/all/{siteId}";

            var client = await HttpClient();

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong, try again");
            }

            var forms = await response.Content
                .ReadFromJsonAsync<List<FormDto>>();

            AppState.Forms.SetFormsCollectionState(forms);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
        finally
        {
            AppState.Forms.InverseFetchingState();
        }
    }
    
    public async Task GetIdAsync(int id)
    {
        try
        {
            AppState.Forms.InverseFetchingState();

            var url = $"/gateway/forms/{id}";

            var client = await HttpClient();

            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong, try again");
            }

            var form = await response.Content
                .ReadFromJsonAsync<FormDto>();

            AppState.Forms.SetFormState(form);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
        }
        finally
        {
            AppState.Forms.InverseFetchingState();
        }
    }

    public async Task<List<FormResponseDto>> GetAllResponsesByFormIdAsync
        (int formId)
    {
        var url = $"/gateway/formresponse/all/{formId}";

        var client = await HttpClient();

        var response = await client
            .GetFromJsonAsync<ResponseModel<List<FormResponseDto>>>(url);

        return response is null ? new List<FormResponseDto>() : response.Results;
    }

    public async Task CreateAsync(FormDto form)
    {
        try
        {
            const string url = "/gateway/forms";

            var client = await HttpClient();

            var response = await client.PostAsJsonAsync(url, form);

            if (response.StatusCode == HttpStatusCode.Forbidden)
                throw new ForbiddenException("Access is not allowed");

            if (response.IsSuccessStatusCode)
            {
                var userId = AppState.Identity.User.Id;

                _navigationManager.NavigateTo($"/dashboard/forms?user_id={userId}&site_id={form.SiteId}");
            }
            else
            {
                throw new Exception();
            }
        }
        catch (ForbiddenException exception)
        {
            _navigationManager.NavigateTo("/dashboard/error?error_code=403&error_message=max_allowed_container");

            AppState.Containers.SetFeaturesState(true);
        }
        catch (Exception exception)
        {
            _navigationManager.NavigateTo($"/dashboard/error?error_code=500&error_message=something_wrong");
        }
        finally
        {
            AppState.Containers.IsFetching = false;
        }
    }
    
    public async Task UpdateAsync(FormDto form)
    {
        try
        {
            const string url = "/gateway/forms";

            var client = await HttpClient();

            var response = await client.PutAsJsonAsync(url, form);

            if (response.IsSuccessStatusCode)
            {
                var userId = AppState.Identity.User.Id;

                _navigationManager.NavigateTo($"/dashboard/forms?user_id={userId}&site_id={form.SiteId}");
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
            AppState.Containers.IsFetching = false;
        }
    }
    
    public async Task DeleteAsync(int id, int redirectSiteId)
    {
        try
        { 
            var url = $"/gateway/forms/{id}";

            var client = await HttpClient();

            var response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var userId = AppState.Identity.User.Id;

                _navigationManager.NavigateTo($"/dashboard/forms?user_id={userId}&site_id={redirectSiteId}");
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
            AppState.Containers.IsFetching = false;
        }
    }
}
