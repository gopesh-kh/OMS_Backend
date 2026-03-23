using Microsoft.EntityFrameworkCore;
using OMS_Backend.Data;
using OMS_Backend.Utils;
using System.Linq.Expressions;

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

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            if (Guard.IsNull(predicate))
                return null;

            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            if (Guard.IsNull(predicate))
                return Enumerable.Empty<T>();

            return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<bool> AnyExistAsync(Expression<Func<T, bool>> predicate)
        {
            if (Guard.IsNull(predicate))
                return false;

            return await _dbSet.AnyAsync(predicate);
        }

        public async Task<(IEnumerable<T> Data, int TotalCount)> GetPagedAsync(
            QueryParams queryParams, Expression<Func<T, bool>>? filter = null)
        {
            var query = _dbSet.AsQueryable();

            if (!Guard.IsNull(filter))
                query = query.Where(filter!);

            var totalCount = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(queryParams.SortBy))
            {
                try
                {
                    query = queryParams.IsDescending
                        ? query.OrderByDescending(e => EF.Property<object>(e, queryParams.SortBy))
                        : query.OrderBy(e => EF.Property<object>(e, queryParams.SortBy));
                }
                catch
                {
                    query = query.OrderBy(e => 0);
                }
            }

            var data = await query
                .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .AsNoTracking()
                .ToListAsync();

            return (data, totalCount);
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

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}