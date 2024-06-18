using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace fast_food.Models
{
    public class CartItem
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ItemId")]
        public Guid ItemId { get; set; }

        public Item Item { get; set; }

        [ForeignKey("CartId")]
        public Guid CartId { get; set; }

        public Cart Cart { get; set; }
    }
}
