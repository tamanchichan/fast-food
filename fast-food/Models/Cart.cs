using System.ComponentModel.DataAnnotations;

namespace fast_food.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }

        public HashSet<CartItem> Items { get; set;} = new HashSet<CartItem>();

        // Calculates the subtotal by summing up the prices of all items in the cart
        public decimal SubTotal
        {
            get { return Items.Sum(i => i.Price); }
        }

        // Calculates the total by adding a 12% tax to the subtotal
        public decimal Total
        {
            get { return SubTotal * 1.12m; }
        }
    }
}
