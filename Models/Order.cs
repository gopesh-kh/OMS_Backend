using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OMS_Backend.Models
{
    public enum OrderStatus
    {
        PENDING,     // 0
        SHIPPED,    // 1
        DELIVERED, // 2 
        CANCELLED // 3
    }

    public class Order : BaseEntity
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate {  get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ShippingAddressId { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }
        public User? User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Address? ShippingAddress { get; set; }
    }
}
