using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using OMS_Backend.DTOs.Product;
using OMS_Backend.Models;
using OMS_Backend.Repositories;

namespace OMS_Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository repository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _productRepository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductResponseDto>> GetProductsAsync(
            int? categoryId,
            string? search,
            string? sortBy,
            bool isDescending,
            int pageNumber,
            int numberOfProductsPerPage)
        {
            var products = await _productRepository.GetProductsAsync(
                categoryId,
                search,
                sortBy,
                isDescending,
                pageNumber,
                numberOfProductsPerPage);

            return _mapper.Map<List<ProductResponseDto>>(products);
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository
                .GetByIdAsync(id);

            if (product == null)
                return null;

            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            if (dto.CategoryIds != null && dto.CategoryIds.Any())
            {
                var categories = await _categoryRepository
                    .GetCategoriesByIdsAsync(dto.CategoryIds);

                foreach (var category in categories)
                {
                    product.Categories.Add(category);
                }
            }

            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();

            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            if (id == null || id <= 0) 
                return false;

            if (dto == null)
                return false;

            var product = await _productRepository
                .GetByIdAsync(id);

            if (product == null)
                return false;

            _mapper.Map(dto, product);

            // Update Categories
            product.Categories.Clear();

            if (dto.CategoryIds != null && dto.CategoryIds.Any())
            {
                var categories = await _categoryRepository
                    .GetCategoriesByIdsAsync(dto.CategoryIds);

                foreach (var category in categories)
                {
                    product.Categories.Add(category);
                }
            }

            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return false;

            _productRepository.Delete(product);
            await _productRepository.SaveAsync();

            return true;
        }
    }
}