using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        public string? ProductImage { get; set; }

        public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
        public ICollection<ProductReview> ProductReview { get; set; } = new List<ProductReview>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
