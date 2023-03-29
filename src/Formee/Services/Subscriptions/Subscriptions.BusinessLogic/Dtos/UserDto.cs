namespace Subscriptions.BusinessLogic.Dtos;

public class UserDto : BaseDto
{
    public Guid GlobalUserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public static implicit operator UsersModel(UserDto user)
    {
        var userModel = new UsersModel
        {
            Id = user.Id,
            GlobalUserId = user.GlobalUserId,
            UserName = user.UserName,
            Email = user.Email,
            Role = user.Role,
            CreatedDate = user.CreatedDate
        };

        return userModel;
    }
}
