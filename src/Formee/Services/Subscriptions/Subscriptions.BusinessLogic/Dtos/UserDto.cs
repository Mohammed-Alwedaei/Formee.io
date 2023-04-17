namespace Subscriptions.BusinessLogic.Dtos;

public class UserDto : BaseDto
{
    public Guid GlobalUserId { get; set; }

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public static implicit operator UserDto(UsersModel user)
    {
        return new UserDto
        {
            Id = user.Id,
            GlobalUserId = user.GlobalUserId,
            Email = user.Email,
            Role = user.Role,
            CreatedDate = user.CreatedDate
        };
    }
}
