namespace Subscriptions.BusinessLogic.Repositories.IRepository;

public interface IOrderRepository
{
    Task<OrderHeaderDto?> GetByIdAsync(int id);

    Task<List<OrderHeaderDto>> GetAllByUserIdAsync(int userId);

    Task<OrderHeaderDto?> CreateAsync(OrderDetailsDto orderDetails, int userId);
}
