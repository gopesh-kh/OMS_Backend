using Microsoft.EntityFrameworkCore;
using OMS_Backend.Data;
using OMS_Backend.Models;
using OMS_Backend.Utils;

namespace OMS_Backend.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartWithItemsAsync(int userId)
        {
            if (Guard.IsInvalidId(userId))
                return null;

            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}