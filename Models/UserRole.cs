using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } = string.Empty;
    }
}
