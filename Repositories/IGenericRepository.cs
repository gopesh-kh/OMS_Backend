using OMS_Backend.Utils;
using System.Linq.Expressions;

namespace OMS_Backend.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);

        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<bool> AnyExistAsync(Expression<Func<T, bool>> predicate);
        Task<(IEnumerable<T> Data, int TotalCount)> GetPagedAsync(
            QueryParams queryParams, 
            Expression<Func<T, bool>>? filter = null);

        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task SaveAsync();
    }
}