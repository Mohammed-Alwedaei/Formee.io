using Client.Web.Utilities.Dtos.Links;

namespace Client.Web.Utilities.StateManagement;

public class LinksState
{
    public List<LinkDto> LinksCollection;
    
    public LinkDto Link;
    public List<LinkHitDto> LinkHitsCollection;
    public List<LinkHitDto> LinkHitsInContainerCollection;

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
    
    public void SetLinkHitsState(List<LinkHitDto> linkHits)
    {
        LinkHitsCollection = linkHits;
        StateChanged.Invoke();
    }
    public void SetLinkHitsInContainerCollectionState(List<LinkHitDto> linkHits)
    {
        LinkHitsInContainerCollection = linkHits;
        StateChanged.Invoke();
    }
    
    public void InvertFetchingState() => IsFetching = !IsFetching;
}