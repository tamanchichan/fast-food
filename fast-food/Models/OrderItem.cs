using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace fast_food.Models
{
    public class OrderItem
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

        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }

        public Order Order { get; set; }
    }
}
