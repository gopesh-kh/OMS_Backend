using OMS_Backend.Models;

namespace OMS_Backend.Repositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart?> GetCartWithItemsAsync(int userId);
    }
}