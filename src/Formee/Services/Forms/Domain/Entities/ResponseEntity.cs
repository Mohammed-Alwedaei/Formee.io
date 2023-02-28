namespace Domain.Entities;

/// <summary>
/// ResponseEntity visualizes the response of the Mediator results
/// </summary>
public class ResponseEntity
{
    public bool IsSuccessRequest { get; set; } = false;

    public object? Results { get; set; }

    public string? Error { get; set; }

    [JsonPropertyName("__timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.Now;
}
