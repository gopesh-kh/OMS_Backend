using OMS_Backend.DTOs.User;
using OMS_Backend.Models;
using OMS_Backend.Utils;

public interface IUserService
    : IGenericService<User, UserResponseDto, UpdateUserDto>
{
    Task<(IEnumerable<UserResponseDto>, int)> GetUsersAsync(QueryParams query);
}