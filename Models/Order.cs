using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.Models
{
    public enum Status
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
        public DateTime OrderDate {  get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ShippingAddressId { get; set; }

        [Required]
        public Status Status { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Address ShippingAddress { get; set; }
    }
}
