namespace Domain.Entities;

/// <summary>
/// ResponseEntity visualizes the response of the Mediator results
/// </summary>
public class ResponseEntity
{
    public object? Results { get; set; }


    [JsonPropertyName("__timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.Now;
}
