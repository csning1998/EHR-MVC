using Microsoft.AspNetCore.Mvc;
using EHR_MVC.Models;

namespace EHR_MVC.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/Status401")]
        public IActionResult Status401()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier,
            };
            return View("~/Views/Error/Error.cshtml", errorViewModel);
        }

        [Route("Error/Unauthorized")]
        public IActionResult Unauthorized()
        {
            var errorViewModel = new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier,
            };
            return View("~/Views/Error/Unauthorized.cshtml", errorViewModel);
        }
    }
}
