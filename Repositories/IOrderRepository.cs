using OMS_Backend.Models;

namespace OMS_Backend.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    }
}