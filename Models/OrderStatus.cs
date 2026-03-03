using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.Models
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = string.Empty;
    }
}
