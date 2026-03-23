using AutoMapper;
using OMS_Backend.Repositories;
using OMS_Backend.Utils;

namespace OMS_Backend.Services
{
    public class GenericService<TEntity, TResponseDto, TUpdateDto>
        : IGenericService<TEntity, TResponseDto, TUpdateDto>
        where TEntity : class
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public GenericService(
            IGenericRepository<TEntity> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<TResponseDto>, int)> GetPagedAsync(QueryParams query)
        {
            var (data, total) = await _repository.GetPagedAsync(query);

            var mapped = _mapper.Map<IEnumerable<TResponseDto>>(data);

            return (mapped, total);
        }

        public async Task<TResponseDto?> GetByIdAsync(int id)
        {
            if (Guard.IsInvalidId(id))
                return default;

            var entity = await _repository.GetByIdAsync(id);

            if (Guard.IsNull(entity))
                return default;

            return _mapper.Map<TResponseDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, TUpdateDto dto)
        {
            if (Guard.IsInvalidId(id) || Guard.IsNull(dto))
                return false;

            var entity = await _repository.GetByIdAsync(id);

            if (Guard.IsNull(entity))
                return false;

            _mapper.Map(dto, entity);

            _repository.Update(entity!);
            await _repository.SaveAsync();

            return true;
        }
    }
}