using System.Text.Json.Serialization;

namespace Client.Web.Utilities.Models;

public class ResponseModel<T>
{
    public T Results { get; set; }

    [JsonPropertyName("__timestamp")]
    public DateTime Timestamp { get; set; }
}
