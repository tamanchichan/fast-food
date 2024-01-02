using fast_food.Areas.Identity.Data;
using fast_food.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace fast_food.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FastFoodDb _context;

        public HomeController(ILogger<HomeController> logger, FastFoodDb context)
        {
            _logger = logger;
            _context = context;
        }

        // Index's view, display a list of 'Item' ordered by 'Item.Code'
        public IActionResult Index()
        {
            List<Item> items = _context.Item.OrderBy(i => i.Code).ToList();

            return View(items);
        }

        // ItemDetails' view, display an 'Item' based of 'Item.Id'
        [Route("item-details/{id}")]
        public IActionResult ItemDetails(Guid id)
        {
            Item item = _context.Item.First(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentOutOfRangeException(nameof(item));
            }

            return View(item);
        }

        // Cart's view
        [Route("cart")]
        public IActionResult Cart()
        {
            Cart cart = _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Item)
                .FirstOrDefault();

            return View(cart);
        }

        // AddToCart's view, create a 'CartItem', or if exists already, increment 'CartItem.Quantity'
        public IActionResult AddToCart(Guid id)
        {
            Item item = _context.Item.First(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentOutOfRangeException(nameof(item));
            }

            CartItem cartItem = _context.CartItems.First(i => i.ItemId == id);

            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    Id = Guid.NewGuid(),
                    ItemId = id,
                    Quantity = 1,
                };
            } else
            {
                cartItem.Quantity++;
            }

            _context.SaveChanges();

            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}