﻿namespace Analytics.Utilities.Dtos.Site;

public class CreateSiteDto
{
    public string? ContainerId { get; set; }

    public Guid UserId { get; set; }

    public string? Domain { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public int CategoryId { get; set; }
}
