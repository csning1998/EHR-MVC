using EHR_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.Design;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection.Metadata.Ecma335;

namespace EHR_MVC.Controllers
{
    public class PatientController : Controller
    {
        private readonly string ConnStr = "Data Source=CSNING\\SQLEXPRESS;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Command Timeout=0";

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

        #region SaveItem
        public async Task<IActionResult> Save([FromForm] PatientViewModel patientViewModel)
        {
            try
            {
                var patientDBModel = ConvertPatientViewModeltoDBModel(patientViewModel);
                var dbResult = await InsertPatientAsync(patientDBModel);

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
        #endregion

        #region Insert
        public async Task<long> InsertPatientAsync(PatientDBModel patient)
        {
            long insertId = 0;

            using (SqlConnection connection = new SqlConnection(ConnStr))
            {
                var insertStr = @"INSERT INTO [EHR-MVC-DB].[dbo].[Patient]
                          VALUES (@IdNo, @Active, @FamilyName, @GivenName, @Telecom, @Gender, @Birthday, @Address);
                          SELECT @InsertId = SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(insertStr, connection))
                {
                    SqlParameter outPutValue = new SqlParameter("@InsertId", SqlDbType.BigInt)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outPutValue);
                    command.Parameters.Add(new SqlParameter("@IdNo", patient.IdNo));
                    command.Parameters.Add(new SqlParameter("@Active", patient.Active));
                    command.Parameters.Add(new SqlParameter("@FamilyName", patient.FamilyName));
                    command.Parameters.Add(new SqlParameter("@GivenName", patient.GivenName));
                    command.Parameters.Add(new SqlParameter("@Telecom", patient.Telecom));
                    command.Parameters.Add(new SqlParameter("@Gender", patient.Gender));
                    command.Parameters.Add(new SqlParameter("@Birthday", patient.Birthday.ToString("yyyy/MM/dd")));
                    command.Parameters.Add(new SqlParameter("@Address", patient.Address));

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    connection.Close();

                    if (outPutValue.Value != DBNull.Value)
                    {
                        insertId = Convert.ToInt64(outPutValue.Value);
                    }
                }
            }

            return insertId;
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
