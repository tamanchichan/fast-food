using System.ComponentModel.DataAnnotations;

namespace fast_food.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public HashSet<CartItem>? CartItems { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "SubTotal can not be less than zero")]
        public decimal SubTotal
        {
            get
            {
                return Math.Round(CartItems.Sum(ci => ci.Item.Price * ci.Quantity), 2);
            }
        }

        [Range(0, double.MaxValue, ErrorMessage = "Total can not be less than zero")]
        public decimal Total
        {
            get
            {
                return Math.Round((decimal)SubTotal * 1.12m, 2); // '1.12m' represents the tax
            }
        }
    }
}
