﻿namespace Client.Web.Utilities.Dtos.Subscriptions;

public class SubscriptionFeatureDto : BaseDto
{
    public string? Name { get; set; }

    public int NumberOfContainers { get; set; }

    public int NumberOfSites { get; set; }

    public int NumberOfForms { get; set; }

    public int NumberOfLinks { get; set; }
}
