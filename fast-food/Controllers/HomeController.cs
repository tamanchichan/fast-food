using fast_food.Data;
using fast_food.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace fast_food.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly FastFoodDbContext _context;

        public HomeController(ILogger<HomeController> logger, FastFoodDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            int code;

            List<Item> items = _context.Item
                //.OrderBy(i => i.Code)
                .ToList();


            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return View(items);
        }

        public IActionResult Cart()
        {
            Cart cart = _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(i => i.Item)
                .FirstOrDefault();

            if (cart == null)
            {
                cart = new Cart() { Id = Guid.NewGuid() };

                _context.Cart.Add(cart);
                _context.SaveChanges();
            }

            return View(cart);
        }

        public IActionResult AddItemToCart(Guid id)
        {
            Cart cart = _context.Cart.FirstOrDefault();

            if (cart == null)
            {
                cart = new Cart() { Id = Guid.NewGuid() };
            }

            Item item = _context.Item.FirstOrDefault(i => i.Id == id);

            if (item == null)
            {
                throw new ArgumentOutOfRangeException(nameof(item));
            }

            CartItem cartItem = _context.CartItem.FirstOrDefault(ci => ci.ItemId == id);

            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    Id = Guid.NewGuid(),
                    ItemId = id,
                    Item = item,
                    CartId = cart.Id,
                    Cart = cart,
                    Quantity = 1
                };

                cart.CartItems.Add(cartItem);

                _context.CartItem.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += 1;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult IncrementCartItem(Guid id)
        {
            CartItem cartItem = _context.CartItem.FirstOrDefault(ci => ci.Id == id);

            if (cartItem == null)
            {
                throw new ArgumentOutOfRangeException(nameof(cartItem));
            }

            cartItem.Quantity += 1;

            _context.SaveChanges();

            return RedirectToAction("Cart");
        }

        public IActionResult DecrementCartItem(Guid id)
        {
            Cart cart = _context.Cart.FirstOrDefault();

            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            CartItem cartItem = _context.CartItem.FirstOrDefault(ci => ci.Id == id);

            if (cartItem == null)
            {
                throw new ArgumentOutOfRangeException(nameof(cartItem));
            }

            if (cartItem.Quantity == 1)
            {
                cart.CartItems.Remove(cartItem);

                _context.CartItem.Remove(cartItem);
            }

            cartItem.Quantity -= 1;

            _context.SaveChanges();

            return RedirectToAction("Cart");
        }

        public IActionResult ClearCart()
        {
            Cart cart = _context.Cart
                .Include(c => c.CartItems)
                .FirstOrDefault();

            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            foreach (CartItem cartItem in cart.CartItems)
            {
                cart.CartItems.Remove(cartItem);

                _context.CartItem.Remove(cartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("Cart");
        }

        public IActionResult Order()
        {
            HashSet<Order> orders = _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .OrderByDescending(o => o.DateOfCreation)
                .ToHashSet();

            return View(orders);
        }

        public IActionResult PlaceOrder()
        {
            Cart cart = _context.Cart
                .Include(c => c.CartItems)
                .FirstOrDefault();

            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            Order order = new Order()
            {
                Id = Guid.NewGuid(),
                DateOfCreation = DateTime.Now
            };

            if (cart.CartItems.Count == 0)
            {
                throw new ArgumentNullException(nameof(cart.CartItems));
            }
            else
            {
                foreach (CartItem cartItem in cart.CartItems)
                {
                    OrderItem orderItem = new OrderItem()
                    {
                        Id = Guid.NewGuid(),
                        ItemId = cartItem.ItemId,
                        Item = cartItem.Item,
                        Order = order,
                        OrderId = order.Id,
                        Quantity = cartItem.Quantity,
                    };

                    order.OrderItems.Add(orderItem);

                    _context.OrderItem.Add(orderItem);

                    cart.CartItems.Remove(cartItem);

                    _context.CartItem.Remove(cartItem);
                }

                _context.Order.Add(order);

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public IActionResult OrderDetails(Guid id)
        {
            Order order = _context.Order
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Item)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return View(order);
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