using Microsoft.EntityFrameworkCore;
using OMS_Backend.Data;
using OMS_Backend.Models;
using OMS_Backend.Utils;

namespace OMS_Backend.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<User> _dbSet;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public async Task<User> RegisterAsync(User user)
        {
            if (Guard.IsNull(user)) { throw new ArgumentNullException("Please provide user details."); }

            await _dbSet.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            if (Guard.IsNullOrWhiteSpace(email)) { throw new ArgumentNullException("Please provide email."); }

            return await _dbSet
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet
                .AnyAsync(u => u.Email == email);
        }
    }
}