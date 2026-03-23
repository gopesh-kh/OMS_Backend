using AutoMapper;
using OMS_Backend.DTOs.User;
using OMS_Backend.Models;
using OMS_Backend.Repositories;
using OMS_Backend.Utils;

namespace OMS_Backend.Services
{
    public class UserService
        : GenericService<User, UserResponseDto, UpdateUserDto>, IUserService
    {
        public UserService(
            IGenericRepository<User> repository,
            IMapper mapper)
            : base(repository, mapper)
        {
        }

        public async Task<(IEnumerable<UserResponseDto>, int)> GetUsersAsync(QueryParams query)
        {
            var (data, total) = await _repository.GetPagedAsync(
                query,
               u => string.IsNullOrWhiteSpace(query.Search) 
               || u.FirstName.Contains(query.Search) 
               || u.LastName.Contains(query.Search));
            return (_mapper.Map<IEnumerable<UserResponseDto>>(data), total);
        }
    }
}