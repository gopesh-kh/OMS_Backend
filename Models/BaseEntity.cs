using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.Models
{
    public class BaseEntity
    {
        [Required]
        public string CreatedBy { get; set; } = String.Empty;

        [Required]
        public DateTime CreatedAt { get; set; }

        public string? ModifiedBy { get; set; } = String.Empty;
        public DateTime? ModifiedAt { get; set; }
    }
}
