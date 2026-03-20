namespace OMS_Backend.DTOs.Order
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public int ShippingAddressId { get; set; }
    }
}