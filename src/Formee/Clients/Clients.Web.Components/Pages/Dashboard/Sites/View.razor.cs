﻿using Client.Web.Utilities.Dtos.Forms;
using Client.Web.Utilities.Services;

namespace Clients.Web.Components.Pages.Dashboard.Sites;

[Route(Routes.ViewSites)]
public partial class View
{
    [Inject]
    public AnalyticsService AnalyticsService { get; set; }

    [Inject]
    public FormsService FormsService { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "id")]
    public int Id { get; set; }

    private SiteDto _site { get; set; }

    private List<FormDto> _forms { get; set; }

    private List<FormResponseDto> _formResponses { get; set; }

    private List<PageHitDto> _pageHits { get; set; }

    private bool IsFetching { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsFetching = true;

        //_site = await AnalyticsService.GetSiteByIdAsync(Id);

        //_forms = await FormsService.GetAllBySiteIdAsync(_site.Id);

        //_formResponses = await FormsService.GetAllResponsesByFormIdAsync(1);

        //var startDate = DateTime.Now.AddMonths(-1);
        //var endDate = DateTime.Now;

        //_pageHits = await AnalyticsService
        //    .GetAllHitsInTimePeriodAsync(Id, startDate, endDate);

        IsFetching = false;
    }
}
