using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.DTOs.Product
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 150 characters.")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string ProductDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 1000000, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
        public int StockQuantity { get; set; }

        [Url(ErrorMessage = "Product image must be a valid URL.")]
        public string? ProductImage { get; set; }

        [Required(ErrorMessage = "At least one category is required.")]
        [MinLength(1, ErrorMessage = "At least one category must be selected.")]
        public List<int> CategoryIds { get; set; } = new();
    }
}