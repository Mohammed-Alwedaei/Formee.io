namespace Subscriptions.BusinessLogic.Models.Users;

public class UserModel : BaseModel
{
    public Guid GlobalUserId { get; set; }

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;
}
