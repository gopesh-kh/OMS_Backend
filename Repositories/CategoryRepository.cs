using Microsoft.EntityFrameworkCore;
using OMS_Backend.Data;
using OMS_Backend.Models;

namespace OMS_Backend.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesByIdsAsync(List<int> ids)
        {
            return await _context.Categories
                .Where(c => ids.Contains(c.CategoryId))
                .ToListAsync();
        }
    }
}
