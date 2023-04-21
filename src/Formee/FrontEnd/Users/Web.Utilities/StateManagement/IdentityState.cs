using Client.Web.Utilities.Dtos.Identity;

namespace Client.Web.Utilities.StateManagement;

public class IdentityState
{
    public UserDto User;

    public bool IsFetching;
    
    public event Action StateChanged;

    public IdentityState()
    {
        User = new UserDto();
    }

    public void SetUserState(UserDto user)
    {
        User = user;
        StateChanged.Invoke();
    }
}