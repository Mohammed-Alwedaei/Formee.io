using Client.Web.Utilities.Dtos.Links;

namespace Client.Web.Utilities.StateManagement;

public class LinksState
{
    public List<LinkDto> LinksCollection;
    
    public LinkDto Link;
    public List<DateChartModel> LinkHitsCollection;
    public List<DateChartModel> LinkHitsInContainerCollection;

    public bool IsFetching;

    public event Action StateChanged;

    public void SetLinksCollectionState(List<LinkDto> links)
    {
        LinksCollection = links;
        StateChanged.Invoke();
    }

    public void SetLinkState(LinkDto link)
    {
        Link = link;
        StateChanged.Invoke();
    }
    
    public void SetLinkHitsState(List<DateChartModel> linkHits)
    {
        LinkHitsCollection = linkHits;
        StateChanged.Invoke();
    }
    public void SetLinkHitsInContainerCollectionState(List<DateChartModel> linkHits)
    {
        LinkHitsInContainerCollection = linkHits;
        StateChanged.Invoke();
    }
    
    public void InvertFetchingState() => IsFetching = !IsFetching;
}