using System.ComponentModel.DataAnnotations;

namespace fast_food.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        [MinLength(3, ErrorMessage = "Name has to be at least 3 characters long")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price can not be less than zero")]
        public decimal Price { get; set; }
    }
}
