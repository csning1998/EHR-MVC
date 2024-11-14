using EHR_MVC.Services;
using EHR_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EHR_MVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        public IActionResult Index()
        {
            var patientViewModel = new PatientViewModel
            {
                IdNo = string.Empty,
                FamilyName = string.Empty,
                GivenName = string.Empty,
                Telecom = string.Empty,
                Gender = string.Empty,
                Address = string.Empty
            };

            var genderCodeList = new List<SelectListItem> {
                new() { Text = "Male", Value = "M" },
                new() { Text = "Female", Value = "F" }
            };

            ViewBag.GenderCodeList = genderCodeList;
            ViewBag.PatientViewModel = patientViewModel;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromForm] PatientViewModel patientViewModel)
        {
            try
            {
                if (patientViewModel == null)
                {
                    ModelState.AddModelError("", "Invalid data submitted.");
                    return View("Index", patientViewModel);
                }

                if (!ModelState.IsValid)
                {
                    return View("Index", patientViewModel);
                }

                var patientDBModel = _patientService.ConvertViewModelToDBModel(patientViewModel);
                var dbResult = await _patientService.InsertPatientAsync(patientDBModel);

                if (dbResult > 0)
                {
                    return Ok(dbResult);
                }

                return BadRequest(dbResult);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    StatusCode = 500,
                    Status = "Error",
                    Error = ex.Message
                });
            }
        }
    }
}
