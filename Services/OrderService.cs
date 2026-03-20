using AutoMapper;
using OMS_Backend.DTOs.Order;
using OMS_Backend.Models;
using OMS_Backend.Repositories;
using OMS_Backend.Utils;

namespace OMS_Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<OrderResponseDto?> CreateOrderAsync(CreateOrderDto dto)
        {
            if (Guard.IsNull(dto) || Guard.IsInvalidId(dto.UserId))
                return null;

            var cart = await _cartRepository.GetCartWithItemsAsync(dto.UserId);

            if (Guard.IsNull(cart) || Guard.IsNullOrEmptyCollection(cart?.CartItems))
                return null;

            var order = new Order
            {
                UserId = dto.UserId,
                ShippingAddressId = dto.ShippingAddressId,
                OrderDate = DateTime.UtcNow,
                OrderStatusId = 1
            };

            decimal totalAmount = 0;

            foreach (var item in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    PriceAtPurchase = item.UnitPrice
                };

                totalAmount += item.Quantity * item.UnitPrice;
                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = totalAmount;

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveAsync();

            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<List<OrderResponseDto>> GetOrdersByUserAsync(int userId)
        {
            if (Guard.IsInvalidId(userId))
                return new List<OrderResponseDto>();

            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            return _mapper.Map<List<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            if (Guard.IsInvalidId(id))
                return null;

            var order = await _orderRepository.GetByIdAsync(id);

            if (Guard.IsNull(order))
                return null;

            return _mapper.Map<OrderResponseDto>(order);
        }
    }
}