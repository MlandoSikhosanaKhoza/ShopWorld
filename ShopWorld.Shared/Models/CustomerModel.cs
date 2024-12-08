using System.ComponentModel.DataAnnotations;

namespace ShopWorld.Shared.Models
{
    public class CustomerModel
    {
        public int CustomerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string? Surname { get; set; }
        [Required]
        [MaxLength(12)]
        public string? Mobile { get; set; }

    }
}
