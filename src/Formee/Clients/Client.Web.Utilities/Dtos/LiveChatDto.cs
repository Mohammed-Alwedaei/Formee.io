namespace Client.Web.Utilities.Dtos;

public class LiveChatDto
{
    public Guid UserId { get; set; }

    public string CurrentMessage { get; set; }

    public List<string> Messages { get; set; } = new();
}
