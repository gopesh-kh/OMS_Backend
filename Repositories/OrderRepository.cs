using Microsoft.EntityFrameworkCore;
using OMS_Backend.Data;
using OMS_Backend.Models;

namespace OMS_Backend.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Order> _dbset;

        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
            _dbset = context.Set<Order>();
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _dbset
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
