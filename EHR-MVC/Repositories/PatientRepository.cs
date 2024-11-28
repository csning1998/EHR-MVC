using EHR_MVC.DBModels.Patient;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EHR_MVC.Repositories.Patient
{
    public class PatientRepository
    {
        private readonly string _connectionString;

        public PatientRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Insert
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

        // Select
        public async Task<List<PatientDBModel>> QueryPatientList(long? PatientId = null, string? IdNo = null, string? familyName = null, string? givenName = null)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                var result = new List<PatientDBModel>();

                var queryStr = @"SELECT * FROM [EHR-MVC-DB].[dbo].[Patient] WHERE 1=1";
                var param = new List<SqlParameter>();

                param.Add(new SqlParameter("@Active", true));

                if (PatientId != null)
                {
                    queryStr += "and PatientId = @PatientId";
                    param.Add(new SqlParameter("@PatientId", @PatientId));
                }
                if (IdNo != null)
                {
                    queryStr += "and IdNo= @IdNo";
                    param.Add(new SqlParameter("@IdNo", @IdNo));
                }
                if (familyName != null)
                {
                    queryStr += "and familyName = like '%' + @familyName +'%'";
                    param.Add(new SqlParameter("@familyName", familyName));
                }
                if (givenName != null)
                {
                    queryStr += "and givenName = like '%' + @givenName +'%'";
                    param.Add(new SqlParameter("@givenName", @givenName));
                }

                SqlCommand command = new SqlCommand(queryStr, connection);

                foreach (var p in param)
                {
                    command.Parameters.Add(p);
                }

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var patient = new PatientDBModel
                        {
                            PatientId = reader.GetInt64(reader.GetOrdinal("PatientId")),
                            IdNo = reader.GetString(reader.GetOrdinal("IdNo")),
                            Active = reader.GetBoolean(reader.GetOrdinal("Active")),
                            FamilyName = reader.GetString(reader.GetOrdinal("FamilyName")),
                            GivenName = reader.GetString(reader.GetOrdinal("GivenName")),
                            Telecom = reader.GetString(reader.GetOrdinal("Telecom")),
                            Gender = reader.GetString(reader.GetOrdinal("Gender")),
                            Birthday = reader.GetDateTime(reader.GetOrdinal("Birthday")),
                            Address = reader.GetString(reader.GetOrdinal("Address"))
                        };

                        result.Add(patient);
                    }
                }
                else
                {
                    Console.WriteLine("(404) User Data not found.");
                }
                connection.Close();
                return result;
            }
        }

        // Update
        public async Task<bool> UpdatePatientAsync(PatientDBModel patient)
        {
            bool result = false;

            using (SqlConnection connection = new(_connectionString)) {
                var insertStr = @"UPDATE INTO [EHR-MVC-DB].[dbo].[Patient]
                              set @IdNo = IdNo, @Active = Active, @FamilyName = FamilyName, @GivenName = GivenName, @Telecom = Telecom, @Gender = Gender, @Birthday = Birthday, @Address = Address);
                              WHERE PatientId = APatientId";

                using SqlCommand command = new(insertStr, connection);
                SqlParameter outPutValue = new("@InsertId", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(new SqlParameter("@PatientId", patient.PatientId));
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

                var updateResult = command.ExecuteNonQuery();

                if (updateResult > 0)
                {
                    result = true;
                }

                return result;
            }
        }

        // To-do: Delete

    }
}
