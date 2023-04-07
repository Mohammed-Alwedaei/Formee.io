namespace Domain.Interfaces;

public interface IEmailsManager
{
    Task<EmailEntity> GetOneByIdAsync(int id);

    Task<IEnumerable<EmailEntity>> GetAllByUserId(Guid userId);

    Task<EmailEntity> CreateAndSendAsync(EmailEntity entity);
}
