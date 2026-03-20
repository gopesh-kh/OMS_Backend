namespace OMS_Backend.DTOs.Order
{
    public class OrderItemResponseDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceAtPurchase { get; set; }
    }
}