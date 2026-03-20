using OMS_Backend.DTOs.Order;

namespace OMS_Backend.Services
{
    public interface IOrderService
    {
        Task<OrderResponseDto?> CreateOrderAsync(CreateOrderDto dto);
        Task<List<OrderResponseDto>> GetOrdersByUserAsync(int userId);
        Task<OrderResponseDto?> GetOrderByIdAsync(int id);
    }
}