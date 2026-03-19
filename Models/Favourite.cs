using System.ComponentModel.DataAnnotations;

namespace OMS_Backend.Models
{
    public class Favourite
    {
        [Key]
        public int FavouriteId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        public User? User { get; set; }
        public Product? Product { get; set; }
    }
}