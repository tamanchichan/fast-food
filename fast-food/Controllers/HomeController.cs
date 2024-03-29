﻿using fast_food.Areas.Identity.Data;
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

        public IActionResult Index()
        {
            return View();
        }

        // Menu's action, return a view of a list of 'Item' ordered by 'Item.Code'
        [Route("menu")]
        public IActionResult Menu()
        {
            List<Item> items = _context.Item.OrderBy(i => i.Code).ToList();

            //return View(items);

            Cart cart = _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Item)
                .FirstOrDefault();

            CartWithItemVM cartWithItemVM = new CartWithItemVM() { Cart = cart, Items = items.ToHashSet() };

            return View(cartWithItemVM);
        }

        // ItemDetails' action, return a view of 'Item' based on 'Item.Id'
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

        // Cart's action, return a view of 'Cart'
        [Route("cart")]
        public IActionResult Cart()
        {
            Cart cart = _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Item)
                .FirstOrDefault();

            if (cart == null)
            {
                cart = new Cart() { Id = Guid.NewGuid() };
            }

            return View(cart);
        }

        public IActionResult AddCartItem(Guid id)
        {
            Cart cart = _context.Cart
                .Include(c => c.CartItems)
                .FirstOrDefault();

            CartItem cartItem = cart.CartItems.Where(ci => ci.Id == id).FirstOrDefault();

            if (cartItem == null)
            {
                throw new ArgumentOutOfRangeException(nameof(cartItem));
            }

            cartItem.Quantity++;
            _context.SaveChanges();

            return RedirectToAction("Cart");
        }

        // RemoveCartItem's action, decrement 'CartItem.Quantity' from the 'Cart' if the quantity is greater than one, else remove 'CartItem' from the 'Cart'
        public IActionResult RemoveCartItem(Guid id)
        {
            Cart cart = _context.Cart
                .Include(c => c.CartItems)
                .FirstOrDefault();

            CartItem cartItem = cart.CartItems.Where(ci => ci.Id == id).FirstOrDefault();

            if (cartItem == null)
            {
                throw new ArgumentOutOfRangeException(nameof(cartItem));
            }

            if (cartItem.Quantity == 1)
            {
                cart.CartItems.Remove(cartItem);
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
            else
            {
                cartItem.Quantity -= 1;
                _context.SaveChanges();
            }

            return RedirectToAction("Cart");
        }

        // AddToCart's action, create a 'CartItem', or if exists already, increment 'CartItem.Quantity'
        public IActionResult AddToCart(Guid id)
        {
            Cart cart = _context.Cart.FirstOrDefault();

            if (cart == null)
            {
                cart = new Cart() { Id = Guid.NewGuid() };
            }

            CartItem cartItem = _context.CartItems.FirstOrDefault(ci => ci.ItemId == id);

            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    Id = Guid.NewGuid(),
                    ItemId = id,
                    Quantity = 1,
                };

                _context.CartItems.Add(cartItem);

                cart.CartItems.Add(cartItem);
            } else
            {
                cartItem.Quantity++;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // ClearCart's action, remove all 'CartItem' from the 'Cart'
        public IActionResult ClearCart()
        {
            Cart cart = _context.Cart
                .Include(c => c.CartItems)
                .FirstOrDefault();

            if (cart == null)
            {
                throw new ArgumentOutOfRangeException(nameof(cart));
            }

            foreach (CartItem cartItem in cart.CartItems)
            {
                cart.CartItems.Remove(cartItem);

                _context.CartItems.Remove(cartItem);
            }

            _context.SaveChanges();

            return RedirectToAction("Cart");
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