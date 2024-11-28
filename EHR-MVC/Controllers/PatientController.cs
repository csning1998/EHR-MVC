using EHR_MVC.Services;
using EHR_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EHR_MVC.Repositories;

namespace EHR_MVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _patientService;
        private readonly PatientRepository _patientRepository;

        public PatientController(PatientService patientService, PatientRepository patientRepository)
        {
            _patientService = patientService;
            _patientRepository = patientRepository;
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

        public IActionResult Search() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromForm] PatientViewModel patientViewModel)
        {
            try
            {
                var patientDBModel = _patientService.ConvertViewModel2DBModel(patientViewModel);
                var dbResult = false;

                if (patientDBModel.PatientId == 0)
                {
                    dbResult = _patientService.InsertPatientAsync(patientDBModel).Result > 0;
                }
                else
                {
                    dbResult = _patientService.UpdatePatientAsync(patientDBModel).Result;
                }

                if (dbResult)
                {
                    return Ok(dbResult);
                }


                if (patientViewModel == null)
                {
                    ModelState.AddModelError("", "Invalid data submitted.");
                    return View("Index", patientViewModel);
                }

                if (!ModelState.IsValid)
                {
                    return View("Index", patientViewModel);
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

        [HttpGet]
        public async Task<IActionResult> Query(long? patientId, string? idNo, string? familyName, string? givenName)
        {
            Console.WriteLine("Query endpoint hit");

            try
            {
                var resultList = new List<PatientViewModel>();
                var dbResult = await _patientRepository.QueryPatientList(patientId, idNo, familyName, givenName);

                if (dbResult.Any())
                {
                    resultList = dbResult.Select(_patientService.ConvertPatientDBModel2ViewModel).ToList();
                    return Ok(resultList);
                }
                else if (dbResult.Count() == 0) {
                    return Ok("No data is found.");
                } else {
                    return Ok(new List<PatientViewModel>());
                }

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Status = "Error",
                    Error = ex.Message,
                    StatusCode = 500
                });
            }
        }


    }
}
