﻿using System.Text.Json.Serialization;

namespace Client.Web.Utilities.Dtos.History;

public class HistoryDto
{
    [JsonPropertyName("__id")]
    public string Id { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Action { get; set; } = null!;

    public Guid UserId { get; set; } 

    public string Service { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
