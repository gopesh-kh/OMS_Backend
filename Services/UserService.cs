using AutoMapper;
using OMS_Backend.DTOs.User;
using OMS_Backend.Models;
using OMS_Backend.Repositories;
using OMS_Backend.Utils;

namespace OMS_Backend.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<UserResponseDto>, int)> GetUsersAsync(QueryParams query)
        {
            var (data, total) = await _repository.GetPagedAsync(
                query,
                u => Guard.IsNullOrWhiteSpace(query.Search) ||
                     u.FirstName.ToLower().Contains(query.Search!.ToLower()) ||
                     u.Email.ToLower().Contains(query.Search!.ToLower())
            );

            return (_mapper.Map<IEnumerable<UserResponseDto>>(data), total);
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            if (Guard.IsInvalidId(id))
                return null;

            var user = await _repository.GetByIdAsync(id);

            if (Guard.IsNull(user))
                return null;

            return _mapper.Map<UserResponseDto>(user);
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            if (Guard.IsInvalidId(id) || Guard.IsNull(dto))
                return false;

            var user = await _repository.GetByIdAsync(id);

            if (Guard.IsNull(user))
                return false;

            _mapper.Map(dto, user);

            _repository.Update(user!);
            await _repository.SaveAsync();

            return true;
        }
    }
}