using Microsoft.EntityFrameworkCore;
using OMS_Backend.Data;
using OMS_Backend.Models;

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
            if (request == null) return;

            await _dbSet.AddAsync(request);
            await _context.SaveChangesAsync();
        }


        public async Task<User?> UserExistAsync(string email)
        {
            if (String.IsNullOrEmpty(email)) return null;

            var user = await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }
    }
}
