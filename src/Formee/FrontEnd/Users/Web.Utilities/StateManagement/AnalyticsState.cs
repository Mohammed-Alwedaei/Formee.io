﻿namespace Client.Web.Utilities.StateManagement;

public class AnalyticsState
{
    //Raw data collections
    public List<SiteDto> Sites = new();
    public List<PageHitDto> Hits;

    public SiteDto Site;

    //Display collection
    public List<DateChartModel> HitChartDataSeries;
    public List<BarChartModel> TopPerformingCategories;
    
    //Collection meta
    public bool IsFetching;

    //State notifier
    public event Action StateChanged;

    public void SetSitesState(List<SiteDto> sites)
    {
        Sites = sites;
        StateChanged.Invoke();
    }
}