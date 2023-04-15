using Client.Web.Utilities.Dtos.History;
using Client.Web.Utilities.Dtos.Identity;

namespace Client.Web.Utilities.Services;

public class AppStateService
{
    public UserDto User { get; set; } = new();

    public List<HistoryDto>? HistoryCollection;

    public int HistoryCount;

    public event Action OnUserStateChange;

    public event Action OnHistoryStateChange;

    public void SetUserState(UserDto user)
    {
        User = new UserDto();
        User = user;
        OnUserStateChange.Invoke();
    }

    /// <summary>
    /// Set history state of the user
    /// </summary>
    /// <param name="newHistoryState"></param>
    public void SetHistoryState(List<HistoryDto>? newHistoryState)
    {
        HistoryCollection = new List<HistoryDto>();
        HistoryCollection = newHistoryState;
        HistoryCount = HistoryCollection.Count;

        OnHistoryStateChange.Invoke();
    }
}
