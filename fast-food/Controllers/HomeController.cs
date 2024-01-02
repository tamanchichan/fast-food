using fast_food.Areas.Identity.Data;
using fast_food.Models;
using Microsoft.AspNetCore.Mvc;
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
            List<Item> items = _context.Item.OrderBy(i => i.Code).ToList();

            return View(items);
        }

        [Route("item-details/{id}")]
        public IActionResult ItemDetails(Guid id)
        {
            Item item = _context.Item.First(i => i.Id == id);

            if (item == null)
            {
                return Problem();
            }

            return View(item);
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