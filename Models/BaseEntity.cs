using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.Models
{
    public class BaseEntity
    {
        [Required]
        public int? CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
