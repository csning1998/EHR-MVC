using EHR_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection.Metadata.Ecma335;

namespace EHR_MVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly string ConnStr = "Data Source=CSNING\\SQLEXPRESS;Integrated Security=True;Persist Security Info=False;Pooling=False;";

        public IActionResult Index()
        {

            var patientViewModel = new PatientViewModel();
            var genderCodeList = new List<SelectListItem> {
                new SelectListItem { Text = "Male", Value = "M" },
                new SelectListItem { Text = "Female", Value = "F" }
            };

            ViewBag.GenderCodeList = genderCodeList;
            ViewBag.PatientViewModel = patientViewModel;

            return View();
        }

        public IActionResult Save([FromForm] PatientViewModel patientViewModel)
        {
            try
            {
                var patientDBModel = ConvertPatientViewModeltoDBModel(patientViewModel);
                var dbResult = InsertPatient(patientDBModel);

                if (dbResult.Result > 0)
                {
                    return Ok(dbResult.Result);
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

        #region SQL

        public Task<long> InsertPatient(PatientDBModel patient)
        {
            long insertId = 0;

            SqlConnection connection = new SqlConnection(ConnStr);

            var insertStr = @"INSERT INTO DB1.dbo.Patient
                      VALUES (@IdNo, @Active, @FamilyName, @GivenName, @Telecom, @Gender, @Birthday, @Address);
                      SELECT @InsertId = SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(insertStr, connection);

            SqlParameter outPutValue = new SqlParameter("@InsertId", SqlDbType.BigInt);
            outPutValue.Direction = ParameterDirection.Output;

            command.Parameters.Add(outPutValue);
            command.Parameters.Add(new SqlParameter("@IdNo", patient.IdNo));
            command.Parameters.Add(new SqlParameter("@Active", patient.Active));
            command.Parameters.Add(new SqlParameter("@FamilyName", patient.FamilyName));
            command.Parameters.Add(new SqlParameter("@GivenName", patient.GivenName));
            command.Parameters.Add(new SqlParameter("@Telecom", patient.Telecom));
            command.Parameters.Add(new SqlParameter("@Gender", patient.Gender));
            command.Parameters.Add(new SqlParameter("@Birthday", patient.Birthday.ToString("yyyy/MM/dd")));
            command.Parameters.Add(new SqlParameter("@Address", patient.Address));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            if (outPutValue.Value != DBNull.Value)
            {
                insertId = Convert.ToInt64(outPutValue.Value);
            }

            return Task.FromResult(insertId);
        }

        #endregion

        #region Private
        private PatientDBModel ConvertPatientViewModeltoDBModel(PatientViewModel viewModel)
        {
            return new PatientDBModel()
            {
                PatientId = viewModel.PatientId,
                IdNo = viewModel.IdNo,
                Active = viewModel.Active,
                FamilyName = viewModel.FamilyName,
                GivenName = viewModel.GivenName,
                Telecom = viewModel.Telecom,
                Gender = viewModel.Gender,
                Birthday = viewModel.Birthday,
                Address = viewModel.Address
            };
        }


        #endregion
    }
}
