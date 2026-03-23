using OMS_Backend.DTOs.Auth;
using OMS_Backend.DTOs.User;

namespace OMS_Backend.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(CreateUserDto request);
        Task<AuthResponseDto> LoginAsync(LoginUserDto request);
    }
}