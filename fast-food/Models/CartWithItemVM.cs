namespace fast_food.Models
{
    public class CartWithItemVM
    {
        public HashSet<Item> Items { get; set; } = new HashSet<Item>();

        public Cart Cart { get; set; }
    }
}
