using EHR_MVC.Models;
using EHR_MVC.Models.Users;
using EHR_MVC.Models.Patient;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EHR_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Process registration logic here (e.g., save to database)
                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}
