using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.Models
{
    public class Cart : BaseEntity
    {
        [Key]
        public int CartId { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}