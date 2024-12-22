using EHR_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EHR_MVC.Repositories;
using EHR_MVC.Models.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace EHR_MVC.Controllers
{


    public class PatientController(PatientService patientService, PatientRepository patientRepository) : Controller
    {
        private readonly PatientService _patientService = patientService;
        private readonly PatientRepository _patientRepository = patientRepository;

        private static PatientViewModel InitializePatientViewModel()
        {
            return new PatientViewModel
            {
                IdNo = string.Empty,
                FamilyName = string.Empty,
                GivenName = string.Empty,
                Telecom = string.Empty,
                Gender = string.Empty,
                Address = string.Empty,
                Email = string.Empty,
                PostalCode = string.Empty,
                Country = string.Empty,
                PreferredLanguage = string.Empty,
                EmergencyContactName = string.Empty,
                EmergencyContactRelationship = string.Empty,
                EmergencyContactPhone = string.Empty,
                Birthday = DateTime.Today
            };
        }

        private static List<SelectListItem> InitializeGenderCodeList()
        {
            return [
                new() { Text = "Male", Value = "M" },
                new() { Text = "Female", Value = "F" }
            ];
        }


        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize]

        public IActionResult Index()
        {
            ViewBag.GenderCodeList = InitializeGenderCodeList();
            ViewBag.PatientViewModel = InitializePatientViewModel();
            return View();
        }

        public IActionResult Search()
        {
            ViewBag.GenderCodeList = InitializeGenderCodeList();
            ViewBag.PatientViewModel = InitializePatientViewModel();
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
