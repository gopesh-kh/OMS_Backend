using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.Models
{
    public class Product : BaseEntity
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string ProductDescription { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
        public ICollection<Category> Categories { get; set; }
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }
    }
}
