using EHR_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using EHR_MVC.Models.Users;

namespace EHR_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Hash password (you can replace this with a real hashing algorithm)
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var userId = await _userService.RegisterUserAsync(model.UserEmail, hashedPassword);

            if (userId > 0)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Failed to register user.");
            return View(model);
        }
    }
}
