using OMS_Backend.DTOs.User;
using OMS_Backend.Utils;

namespace OMS_Backend.Services
{
    public interface IUserService
    {
        Task<(IEnumerable<UserResponseDto>, int)> GetUsersAsync(QueryParams query);

        Task<UserResponseDto?> GetByIdAsync(int id);

        Task<bool> UpdateAsync(int id, UpdateUserDto dto);
    }
}