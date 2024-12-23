using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace EHR_MVC.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.GenderCodeList = new List<SelectListItem>
            {
                new() { Text = "Male", Value = "M" },
                new() { Text = "Female", Value = "F" }
            };
        }
    }
}
