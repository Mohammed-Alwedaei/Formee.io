using System.Text.Json.Serialization;

namespace Client.Web.Utilities.Dtos.Identity;

public class TokenDto
{
    [JsonPropertyName("access_token")]
    public string? Token { get; set; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }
}
