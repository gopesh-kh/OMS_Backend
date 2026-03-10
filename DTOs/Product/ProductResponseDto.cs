namespace OMS_Backend.DTOs.Product
{
    public class ProductResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string? ProductImage {get; set; } = string.Empty;
        public List<int> CategoryIds { get; set; } = new();
    }
}