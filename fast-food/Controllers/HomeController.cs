using fast_food.Data;
using fast_food.Models;
using Microsoft.AspNetCore.Mvc;
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