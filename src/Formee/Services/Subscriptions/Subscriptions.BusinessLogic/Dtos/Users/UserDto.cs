namespace Subscriptions.BusinessLogic.Dtos.Users;

public class UserDto : BaseDto
{
    public Guid GlobalUserId { get; set; }

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;
}
