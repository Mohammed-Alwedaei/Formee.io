namespace SynchronousCommunication.Dtos;

public class UserSubscriptionDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public UserDto? User { get; set; }

    public int SubscriptionId { get; set; }

    public SubscriptionDto? Subscription { get; set; }

    public DateTime CreatedDate { get; set; }
}