using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

      public IActionResult Index()
        {
            var items = _context.Items.Select(i => new ItemViewModel
            {
                ItemID = i.ItemID,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                CategoryID = i.CategoryID // Assuming you want to display or use the CategoryID
            }).ToList();

            return View(items);
        }
    }
}
