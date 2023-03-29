namespace Domain.Interfaces;

public interface IMessageRepository
{
    Task<List<MessageEntity>> GetAllInSessionAsync(int sessionId);

    Task CreateMessageAsync(MessageEntity message);
}
