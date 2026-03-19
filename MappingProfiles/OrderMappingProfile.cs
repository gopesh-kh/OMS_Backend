using AutoMapper;
using OMS_Backend.DTOs.Order;
using OMS_Backend.Models;

namespace OMS_Backend.MappingProfiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile() {
            CreateMap<Order, OrderResponseDto>();
            CreateMap<OrderItem, OrderItemResponseDto>();
        }
    }
}
