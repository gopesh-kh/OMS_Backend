using Microsoft.EntityFrameworkCore;
using OMS_Backend.Data;
using OMS_Backend.Utils;

namespace OMS_Backend.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            if (Guard.IsInvalidId(id))
                return null;

            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            if (Guard.IsNull(entity))
                throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            if (Guard.IsNull(entity))
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            if (Guard.IsNull(entity))
                throw new ArgumentNullException(nameof(entity));

            _dbSet.Remove(entity);
        }

        public async Task<bool> ExistAsync(int id)
        {
            if (Guard.IsInvalidId(id))
                return false;

            var entity = await GetByIdAsync(id);

            return !Guard.IsNull(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}