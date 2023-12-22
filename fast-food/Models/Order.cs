using System.ComponentModel.DataAnnotations;

namespace fast_food.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public List<Item> Items { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Total can not be less than zero.")]
        public decimal Total { get; set; }
    }
}
