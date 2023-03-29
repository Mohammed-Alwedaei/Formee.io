using System.ComponentModel.DataAnnotations.Schema;
using Subscriptions.BusinessLogic.Dtos;

namespace Subscriptions.BusinessLogic.Models;

public class UsersModel : BaseModel
{
    public Guid GlobalUserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int? SubscriptionId { get; set; }

    [ForeignKey(nameof(SubscriptionId))]
    public SubscriptionsModel? Subscription { get; set; }

    public static implicit operator UserDto(UsersModel user)
    {
        var userDto = new UserDto
        {
            Id = user.Id,
            GlobalUserId = user.GlobalUserId,
            UserName = user.UserName,
            Email = user.Email,
            Role = user.Role,
            CreatedDate = user.CreatedDate
        };

        return userDto;
    }
}
