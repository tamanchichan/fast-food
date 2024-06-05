using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace fast_food.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "'Code' has to be at least 1 character length")]
        [DisplayName("Code")]
        public string Code { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "'Name' has to be at least 3 character length")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        [ForeignKey("CartId")]
        public Guid CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
