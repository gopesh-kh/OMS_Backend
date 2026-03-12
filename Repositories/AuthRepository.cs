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

        public async Task RegisterAsync(User request)
        {
            if (Guard.IsNull(request))
                return;

            if (Guard.IsNullOrEmpty(request.Email))
                return;

            if (Guard.IsNullOrEmpty(request.PasswordHash))
                return;

            await _dbSet.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> UserExistAsync(string email)
        {
            if (Guard.IsNullOrEmpty(email))
                return null;

            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}