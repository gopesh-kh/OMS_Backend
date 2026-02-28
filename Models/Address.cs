using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.Models
{
    public class Address : BaseEntity
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        [StringLength(150)]
        public string AddressLine1 { get; set; } = string.Empty;

        [StringLength(150)]
        public string AddressLine2 { get; set;} = string.Empty;

        [Required]
        [StringLength(50)]
        public string City { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string State { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "PostalCode must be exactly 6 digits.")]
        public string PostalCode { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Country { get; set; } = "India";

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
