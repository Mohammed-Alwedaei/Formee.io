using Client.Web.Utilities.Dtos.Forms;
using Client.Web.Utilities.Services;
using Microsoft.AspNetCore.Authorization;

namespace Clients.Web.Components.Pages.Users.Dashboard.Sites;

[Route(Routes.ViewSites)]
[Authorize]
public partial class View
{
    [Inject]
    public AnalyticsService AnalyticsService { get; set; }

    [Inject]
    public FormsService FormsService { get; set; }

    [Parameter]
    [SupplyParameterFromQuery(Name = "siteId")]
    public int SiteId { get; set; }

    private SiteDto _site { get; set; }

    private List<FormDto> _forms { get; set; }

    private List<FormResponseDto> _formResponses { get; set; }

    private List<PageHitDto> _pageHits { get; set; }

    private bool IsFetching { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsFetching = true;

        //_site = await AnalyticsService.GetSiteByIdAsync(SiteId);

        //_forms = await FormsService.GetAllBySiteIdAsync(_site.Id);

        //_formResponses = await FormsService.GetAllResponsesByFormIdAsync(1);

        //var startDate = DateTime.Now.AddMonths(-1);
        //var endDate = DateTime.Now;

        //_pageHits = await AnalyticsService
        //    .GetAllHitsInTimePeriodAsync(SiteId, startDate, endDate);

        IsFetching = false;
    }
}
