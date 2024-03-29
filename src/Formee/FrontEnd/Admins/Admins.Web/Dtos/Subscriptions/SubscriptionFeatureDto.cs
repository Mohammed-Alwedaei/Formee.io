﻿namespace Admins.Web.Dtos.Subscriptions;

public class SubscriptionFeatureDto : BaseDto
{
    public string? Name { get; set; }

    public int NumberOfContainers { get; set; }

    public int NumberOfSites { get; set; }

    public int NumberOfForms { get; set; }

    public int NumberOfLinks { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime DeletedDate { get; set; }
}
