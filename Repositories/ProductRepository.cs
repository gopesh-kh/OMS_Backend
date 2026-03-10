using Microsoft.EntityFrameworkCore;
using OMS_Backend.Data;
using OMS_Backend.Models;

namespace OMS_Backend.Repositories
{
    using Microsoft.EntityFrameworkCore;

    namespace OMS_Backend.Repositories
    {
        public class ProductRepository : GenericRepository<Product>, IProductRepository
        {
            private readonly AppDbContext _context;

            public ProductRepository(AppDbContext context) : base(context)
            {
                _context = context;
            }

            public async Task<List<Product>> GetProductsAsync(
                int? categoryId,
                string? search,
                string? sortBy,
                bool isDescending,
                int pageNumber,
                int numberOfProductsPerPage)
            {
                var query = _context.Products
                    .AsNoTracking()
                    .Include(p => p.Categories)
                    .AsQueryable();

                //if (categoryId.HasValue)
                //    query = query.Where(p => p.Categories.Any(c => c.CategoryId == categoryId.Value));

                if (!string.IsNullOrWhiteSpace(search))
                    query = query.Where(p =>
                        p.ProductName.Contains(search));

                query = sortBy?.ToLower() switch
                {
                    "price" => isDescending
                        ? query.OrderByDescending(p => p.Price)
                        : query.OrderBy(p => p.Price),

                    "name" => isDescending
                        ? query.OrderByDescending(p => p.ProductName)
                        : query.OrderBy(p => p.ProductName),

                    _ => query.OrderByDescending(p => p.ProductId)
                };

                var product = await query
                    .Skip((pageNumber - 1) * numberOfProductsPerPage)
                    .Take(numberOfProductsPerPage)
                    .ToListAsync();

                return product;
            }
        }
    }
}
