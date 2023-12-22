using System.ComponentModel.DataAnnotations;

namespace fast_food.Models
{
    public class Cart
    {
        public Guid Id { get; set; }

        public List<Item>? Items { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "SubTotal can not be less than zero")]
        public decimal? SubTotal
        {
            get
            {
                return Math.Round(Items.Sum(i => i.Price), 2); // think about 'quantity' later
            }
        }

        [Range(0, double.MaxValue, ErrorMessage = "Total can not be less than zero")]
        public decimal? Total
        {
            get
            {
                return Math.Round((decimal)SubTotal * 1.12m, 2); // '1.12m' represents the tax
            }
        }
    }
}
