using OMS_Backend.Models;

namespace OMS_Backend.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsAsync(
            int? categoryId,
            string? search,
            string? sortBy,
            bool isDescending,
            int pageNumber,
            int pageSize);
    }
}