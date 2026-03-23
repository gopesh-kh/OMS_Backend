using OMS_Backend.Utils;

public interface IGenericService<TEntity, TResponseDto, TUpdateDto>
{
    Task<(IEnumerable<TResponseDto> Data, int Total)> GetPagedAsync(QueryParams query);

    Task<TResponseDto?> GetByIdAsync(int id);

    Task<bool> UpdateAsync(int id, TUpdateDto dto);
}