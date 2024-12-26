using EHR_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EHR_MVC.Repositories;
using EHR_MVC.Models.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using System.Collections.Concurrent;

namespace EHR_MVC.Controllers
{
    public static class FhirJsonCache
    {
        public static Dictionary<string, string> CachedFhirJson = new();
    }

    [ApiController]
    [Route("[controller]/[action]")]
    public class PatientController(PatientService patientService, PatientRepository patientRepository) : Controller
    {
        private readonly PatientService _patientService = patientService;
        private readonly PatientRepository _patientRepository = patientRepository;

        private static PatientViewModel InitializePatientViewModel()
        {
            return new PatientViewModel
            {
                IdNo = string.Empty,
                Active = true,
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
            return new List<SelectListItem>
            {
                new() { Text = "Male", Value = "M" },
                new() { Text = "Female", Value = "F" }
            };
        }


        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize]

        public IActionResult Insert()
        {
            ViewBag.GenderCodeList = InitializeGenderCodeList();
            ViewBag.PatientViewModel = InitializePatientViewModel();
            return View();
        }

        public IActionResult Inquire()
        {
            ViewBag.GenderCodeList = InitializeGenderCodeList();
            ViewBag.PatientViewModel = InitializePatientViewModel();
            return View();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
                    patientDBModel.Active = true;
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

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<IActionResult> Query(long? patientId, string? idNo, string? familyName, string? givenName)
        {
            Console.WriteLine("Query endpoint hit");

            try
            {
                var dbResult = await _patientRepository.QueryPatientList(patientId, idNo, familyName, givenName);

                if (dbResult.Count != 0)
                {
                    var resultList = dbResult.Select(_patientService.ConvertPatientDBModel2ViewModel).ToList();
                    return Ok(resultList);
                }
                else
                {
                    return Ok("No data is found.");
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] long? PatientId)
        {
            if (PatientId == null)
            {
                return BadRequest(new { StatusCode = 400, Message = "PatientId is required." });
            }

            try
            {
                var patientList = await _patientRepository.QueryPatientList(PatientId);

                if (patientList == null || patientList.Count == 0)
                {
                    return NotFound(new { StatusCode = 404, Message = "Patient not found." });
                }

                var patient = patientList.First();

                if (!patient.Active)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Patient is already inactive." });
                }

                patient.Active = false;

                var updateResult = await _patientRepository.UpdatePatientAsync(patient);

                if (updateResult)
                {
                    return Ok(new { StatusCode = 200, Message = "Patient successfully deleted." });
                }
                else
                {
                    return StatusCode(500, new { StatusCode = 500, Message = "Failed to delete patient." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Error = ex.Message,
                    StatusCode = 500
                });
            }
        }


        public class UploadRequest
        {
            public long PatientId { get; set; }
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> GetFhirJson([FromBody] UploadRequest request)
        {
            try
            {
                if (request == null || request.PatientId <= 0)
                {
                    return BadRequest(new { StatusCode = 400, Message = "Invalid PatientId." });
                }

                // Retrieve data from DB
                var patientDBModel = (await _patientRepository.QueryPatientList(PatientId: request.PatientId))
                    .FirstOrDefault();

                if (patientDBModel == null)
                {
                    return NotFound(new { StatusCode = 404, Message = "Patient not found." });
                }

                // Convert to FHIR JSON
                var fhirService = new FhirService();
                var fhirJson = fhirService.ConvertPatientToFhirJson(patientDBModel);

                // Generate a new token stored in the controller
                var token = Guid.NewGuid().ToString();

                // Consider using ConcurrentDictionary, MemoryCache, or Redis
                FhirJsonCache.CachedFhirJson[token] = fhirJson;

                // Send the token and the JSON to frontend
                return Ok(new
                {
                    StatusCode = 200,
                    Token = token,
                    FhirJson = fhirJson
                });
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

        public class TokenRequest
        {
            public string Token { get; set; } = string.Empty;
        }

        [HttpPost]
        //[Route("Patient/SubmitFhirJson")]
        public async Task<IActionResult> SubmitFhirJson([FromBody] TokenRequest request)
        {
            Console.WriteLine($"Received token: {request.Token}");
            try
            {
                // Examine Token
                if (string.IsNullOrEmpty(request.Token))
                {
                    return BadRequest(new { StatusCode = 400, Message = "Token is empty." });
                }

                // 2) Retrieve JSON from the dictionary
                if (!FhirJsonCache.CachedFhirJson.TryGetValue(request.Token, out var fhirJson))
                {
                    return NotFound(new { StatusCode = 404, Message = "Token not found or expired." });
                }

                // 3) Send to HAPI FHIR SERVER
                using var client = new HttpClient { BaseAddress = new Uri("http://localhost:8080/fhir/") };
                var content = new StringContent(fhirJson, Encoding.UTF8, "application/fhir+json");
                var response = await client.PostAsync("/fhir/Patient", content);

                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Delete the token if succeed.
                    FhirJsonCache.CachedFhirJson.Remove(request.Token);
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "FHIR JSON submitted successfully.",
                        HapiResponse = responseBody
                    });
                }
                else
                {
                    return StatusCode((int)response.StatusCode, new
                    {
                        Status = "Error",
                        Message = "Failed to submit FHIR JSON.",
                        HapiError = responseBody
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new
                {
                    StatusCode = 500,
                    Status = "Error",
                    ex.Message
                });
            }
        }
    }
}
