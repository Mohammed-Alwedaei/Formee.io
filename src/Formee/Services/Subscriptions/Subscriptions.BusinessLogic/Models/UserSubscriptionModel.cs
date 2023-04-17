﻿using System.ComponentModel.DataAnnotations;

namespace Subscriptions.BusinessLogic.Models;

public class UserSubscriptionModel
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))] 
    public UsersModel User { get; set; }

    public int SubscriptionId { get; set; }

    [ForeignKey(nameof(SubscriptionId))] 
    public SubscriptionsModel Subscription { get; set; }

    public DateTime CreatedDate { get; set; }

    public static implicit operator UserSubscriptionModel(UserSubscriptionDto userSubscription)
    {
        return new UserSubscriptionDto()
        {
            Id = userSubscription.Id,
            UserId = userSubscription.UserId,
            User = userSubscription.User,
            SubscriptionId = userSubscription.SubscriptionId,
            Subscription = userSubscription.Subscription,
            CreatedDate = userSubscription.CreatedDate
        };
    }
}