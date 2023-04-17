namespace Subscriptions.BusinessLogic.Models;

public class UsersModel : BaseModel
{
    public Guid GlobalUserId { get; set; }

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public static implicit operator UsersModel(UserDto user)
    {
        var userDto = new UserDto
        {
            Id = user.Id,
            GlobalUserId = user.GlobalUserId,
            Email = user.Email,
            Role = user.Role,
            CreatedDate = user.CreatedDate
        };

        return userDto;
    }
}
