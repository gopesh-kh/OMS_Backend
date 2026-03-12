namespace OMS_Backend.Services
{
    public interface IAuthService
    {
        Task<string?> RegisterAsync(CreateUserDto request);
        Task<string?> LoginAsync(LoginUserDto request);
    }
}