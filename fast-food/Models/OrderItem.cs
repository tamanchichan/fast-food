using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace fast_food.Models
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("ItemId")]
        public Guid ItemId { get; set; }

        public Item Item { get; set; }

        [ForeignKey("OrderId")]
        public Guid OrderId { get; set; }

        public Order Order { get; set; }
    }
}
