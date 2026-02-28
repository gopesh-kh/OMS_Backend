using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.Models
{
    public enum Role
    {
        ADMIN,      // 0 -> ADMIN
        VENDOR,    // 1 -> SELLER
        CUSTOMER  // 2 -> BUYER
    }
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public Role Role { get; set; }
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
