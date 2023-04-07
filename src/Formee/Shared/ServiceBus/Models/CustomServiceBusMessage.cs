namespace ServiceBus.Models;

public class CustomServiceBusMessage<TEntity> where TEntity : class
{
    public TEntity Entity { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
