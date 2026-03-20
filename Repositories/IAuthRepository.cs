using OMS_Backend.Models;

namespace OMS_Backend.Repositories
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(User user);

        Task<User?> GetByEmailAsync(string email);

        Task<bool> EmailExistsAsync(string email);
    }
}