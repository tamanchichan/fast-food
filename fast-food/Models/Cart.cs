using System.ComponentModel.DataAnnotations;

namespace fast_food.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }

        public HashSet<CartItem> CartItems { get; set;} = new HashSet<CartItem>();

        // Calculates the subtotal by summing up the prices of all items in the cart
        public decimal SubTotal
        {
            get { return Math.Round(CartItems.Sum(i => i.Item.Price), 2); }
        }

        // Calculates the total by adding a 12% tax to the subtotal
        public decimal Total
        {
            get { return Math.Round(SubTotal * 1.12m, 2); }
        }
    }
}
