using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace fast_food.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayName("Date of Creation")]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public HashSet<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        // Calculates the subtotal by summing up the prices of all items in the cart
        public decimal SubTotal
        {
            get { return Math.Round(OrderItems.Sum(i => i.Item.Price), 2); }
        }

        // Calculates the total by adding a 12% tax to the subtotal
        public decimal Total
        {
            get { return Math.Round(SubTotal * 1.12m, 2); }
        }
    }
}
