using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validate the user's credentials with the database directly
                bool isValidUser = ValidateUser(model.Email, model.Password);

                if (isValidUser)
                {
                    // Set a session variable to indicate the user is logged in
                    HttpContext.Session.SetString("IsLoggedIn", "true");

                    // If user is valid, redirect to another page or set session, etc.
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }

            return View(model);
        }

        private bool ValidateUser(string email, string password)
        {
            // Directly check if the user exists in the database with the given email and password
            User? user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            
            return user != null;
        }

        // GET: Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Here, you would typically hash the password before saving
                var user = new User { Email = model.Email, Password = model.Password, Name = model.Name };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Redirect to login page or wherever appropriate after registration
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Clear the session variable on logout
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
