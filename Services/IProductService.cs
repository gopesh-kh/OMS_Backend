using OMS_Backend.DTOs.Product;

namespace OMS_Backend.Services
{
    public interface IProductService
    {
        Task<List<ProductResponseDto>> GetProductsAsync(
            int? categoryId,
            string? search,
            string? sortBy,
            bool isDescending,
            int pageNumber,
            int pageSize);

        Task<ProductResponseDto?> GetByIdAsync(int id);

        Task<ProductResponseDto> CreateAsync(CreateProductDto dto);

        Task<bool> UpdateAsync(int id, UpdateProductDto dto);

        Task<bool> DeleteAsync(int id);
    }
}