using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddToCart(int itemId, int quantity)
        {
            // add the item to the cart
            // This involves checking if the user already has a cart,
            // adding the item to the cart, and saving the changes.

            return RedirectToAction("Index", "Home");
        }
    }
}
