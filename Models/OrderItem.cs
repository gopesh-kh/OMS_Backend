using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS_Backend.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAtPurchase { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public Order? Order { get; set; }
        public Product? Product { get; set; }

        [NotMapped]
        public decimal SubTotal => Quantity * PriceAtPurchase;
    }
}