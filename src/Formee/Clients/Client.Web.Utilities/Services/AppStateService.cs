using Client.Web.Utilities.Dtos.Identity;

namespace Client.Web.Utilities.Services;

public class AppStateService
{
    public UserDto User { get; set; } = new();

    public void SetUserState(UserDto user)
    {
        User = new UserDto();
        User = user;
    }
}
