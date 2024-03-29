﻿using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Client.Web.Utilities.Dtos.Identity;

namespace Client.Web.Utilities.Services;

public class BaseService
{
    public IHttpClientFactory HttpClientFactory { get; set; }
    public IConfiguration Configuration { get; set; }
    public AppStateService AppState { get; set; }

    private readonly HttpClient _httpClient;

    public BaseService(IHttpClientFactory httpClientFactory, 
        IConfiguration configuration,
        AppStateService appState)
    {
        HttpClientFactory = httpClientFactory;
        Configuration = configuration;
        AppState = appState;

        _httpClient = HttpClientFactory.CreateClient("ServerApi");

        var baseAddress = Configuration["Gateway"];

        if (baseAddress != null)
        {
            _httpClient.BaseAddress = new Uri(baseAddress);
        }
        
    }

    public async Task<HttpClient> HttpClient()
    {
        var accessToken = 
            await GetAccessTokenAsync(AppState.Identity.AuthId);

        _httpClient.DefaultRequestHeaders.Authorization = new
            AuthenticationHeaderValue("Bearer", accessToken);

        return _httpClient;
    }

    private async Task<string?> GetAccessTokenAsync(string authId)
    {
        return await _httpClient
            .GetFromJsonAsync<string>($"/gateway/identity/users/{authId}");
    }
}
