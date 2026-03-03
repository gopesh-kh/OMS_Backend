using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.Models
{
    public class ProductReview : BaseEntity
    {
        [Key]
        public int ProductReviewId { get; set; }

        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [StringLength(1000, ErrorMessage = "Review cannot exceed 1000 characters.")]
        public string? ReviewText { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int UserId { get; set; }

        //public decimal AverageReview { get; set; }

        public Product? Product { get; set; }
        public User? User { get; set; }
    }
}
