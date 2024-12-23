using EHR_MVC.Models.Patient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EHR_MVC.Controllers.Outpatient
{
    public class OutpatientController : BaseController
    {
        public IActionResult Index()
        {
            ViewBag.PatientViewModel = new PatientViewModel
            {
                FamilyName = string.Empty,
                GivenName = string.Empty,
                Gender = string.Empty,
                IdNo = string.Empty,
                Birthday = DateTime.Today,
                Telecom = string.Empty,
                Address = string.Empty,
                Email = string.Empty,
                PostalCode = string.Empty,
                Country = string.Empty,
                PreferredLanguage = string.Empty,
                EmergencyContactName = string.Empty,
                EmergencyContactRelationship = string.Empty,
                EmergencyContactPhone = string.Empty
            };
            return View();
        }

        // GET: OutpatientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OutpatientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OutpatientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OutpatientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OutpatientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OutpatientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OutpatientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
