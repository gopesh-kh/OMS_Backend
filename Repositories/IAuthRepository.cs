using OMS_Backend.Models;

namespace OMS_Backend.Repositories
{
    public interface IAuthRepository
    {
        Task RegisterAsync(User request);
        Task<User?> UserExistAsync(string email);
    }
}