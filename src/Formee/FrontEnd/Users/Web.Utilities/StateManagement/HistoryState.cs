using Client.Web.Utilities.Dtos.History;

namespace Client.Web.Utilities.StateManagement;

public class HistoryState
{
    public List<HistoryDto>? HistoryCollection;

    public bool IsFetching;

    public event Action StateChanged;

    public HistoryState()
    {
        HistoryCollection = new List<HistoryDto>();
    }

    public void SetHistoryCollectionState(List<HistoryDto>? historyCollection)
    {
        HistoryCollection = historyCollection;
        StateChanged.Invoke();
    }
}