using EHR_MVC.Services;
using EHR_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EHR_MVC.Repositories;
using System.Reflection.Metadata.Ecma335;

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

        [HttpGet]
        public IActionResult Query(long? patientId, string? idNo, string? familyName, string? givenName)
        {
            try
            {
                var resultList = new List<PatientViewModel>();
                var dbResult = _patientRepository.QueryPatientList(patientId, idNo, familyName, givenName);

                if (dbResult.Result.Count() > 0)
                {
                    resultList = dbResult.Result.Select(_patientService.ConvertPatientDBModel2ViewModel).ToList();
                    return Ok(dbResult.Result);
                }
                else {
                    return BadRequest(dbResult);
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Status = "Error",
                    Error = ex.Message
                });
            }
        }


    }
}
