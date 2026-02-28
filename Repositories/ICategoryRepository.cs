using OMS_Backend.Models;

namespace OMS_Backend.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetCategoriesByIdsAsync(List<int> ids);
    }
}
