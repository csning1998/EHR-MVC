using EHR_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EHR_MVC.Repositories;
using EHR_MVC.Models.Patient;

namespace EHR_MVC.Controllers.Patient
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

            var patientViewModel = new PatientViewModel
            {
                IdNo = string.Empty,
                FamilyName = string.Empty,
                GivenName = string.Empty,
                Telecom = string.Empty,
                Gender = string.Empty,
                Address = string.Empty,
                Birthday = DateTime.Today
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
                    return BadRequest(new { StatusCode = 400, Message = "Invalid data submitted." });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Model validation failed." });
                }

                var patientDBModel = _patientService.ConvertPatientViewModel2DBModel(patientViewModel);
                bool dbResult;

                if (patientDBModel.PatientId == 0)
                {
                    dbResult = await _patientService.InsertPatientAsync(patientDBModel) > 0;
                }
                else
                {
                    dbResult = await _patientService.UpdatePatientAsync(patientDBModel);
                }

                if (dbResult)
                {
                    return Ok(new { StatusCode = 200, Message = "Operation successful." });
                }
                else
                {
                    return BadRequest(new { StatusCode = 400, Message = "Failed to save patient data." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Status = "Error",
                    ex.Message
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
