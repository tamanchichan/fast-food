using System.ComponentModel.DataAnnotations;

namespace fast_food.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity can not be less than zero.")]
        public int Quantity { get; set; }
    }
}
