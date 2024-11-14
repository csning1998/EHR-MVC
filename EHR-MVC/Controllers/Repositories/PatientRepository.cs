using EHR_MVC.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EHR_MVC.Repositories
{
    public class PatientRepository
    {
        private readonly string _connectionString;

        public PatientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<long> InsertPatientAsync(PatientDBModel patient)
        {
            long insertId = 0;

            using (SqlConnection connection = new(_connectionString))
            {
                var insertStr = @"INSERT INTO [EHR-MVC-DB].[dbo].[Patient]
                              VALUES (@IdNo, @Active, @FamilyName, @GivenName, @Telecom, @Gender, @Birthday, @Address);
                              SELECT @InsertId = SCOPE_IDENTITY();";

                using SqlCommand command = new(insertStr, connection);
                SqlParameter outPutValue = new("@InsertId", SqlDbType.BigInt)
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
            return insertId;
        }
    }
}
