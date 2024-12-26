using EHR_MVC.Models.Patient;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EHR_MVC.Repositories
{
    public class PatientRepository(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        // Insert
        public async Task<long> InsertPatientAsync(PatientDBModel patient)
        {
            long insertId = 0;

            using (SqlConnection connection = new(_connectionString))
            {
                var insertStr = @"
                    INSERT INTO [EHR-MVC-DB].[dbo].[Patient]
                        (IdNo, Active, FamilyName, GivenName, Telecom, Gender, Birthday, Address, 
                            Email, PostalCode, Country, PreferredLanguage, EmergencyContactName, EmergencyContactRelationship, EmergencyContactPhone)
                    VALUES 
                        (@IdNo, @Active, @FamilyName, @GivenName, @Telecom, @Gender, @Birthday, @Address, 
                            @Email, @PostalCode, @Country, @PreferredLanguage, @EmergencyContactName, @EmergencyContactRelationship, @EmergencyContactPhone);
                    SELECT @InsertId = SCOPE_IDENTITY();
                ";

                using SqlCommand command = new(insertStr, connection);
                SqlParameter outPutValue = new("@InsertId", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(outPutValue);
                command.Parameters.Add(new SqlParameter("@IdNo", patient.IdNo));
                command.Parameters.Add(new SqlParameter("@Active", patient.Active)); // 確保 Active 被傳遞
                command.Parameters.Add(new SqlParameter("@FamilyName", patient.FamilyName));
                command.Parameters.Add(new SqlParameter("@GivenName", patient.GivenName));
                command.Parameters.Add(new SqlParameter("@Telecom", patient.Telecom));
                command.Parameters.Add(new SqlParameter("@Gender", patient.Gender));
                command.Parameters.Add(new SqlParameter("@Birthday", patient.Birthday.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@Address", patient.Address));
                command.Parameters.Add(new SqlParameter("@Email", patient.Email));
                command.Parameters.Add(new SqlParameter("@PostalCode", patient.PostalCode));
                command.Parameters.Add(new SqlParameter("@Country", patient.Country));
                command.Parameters.Add(new SqlParameter("@PreferredLanguage", patient.PreferredLanguage));
                command.Parameters.Add(new SqlParameter("@EmergencyContactName", patient.EmergencyContactName));
                command.Parameters.Add(new SqlParameter("@EmergencyContactRelationship", patient.EmergencyContactRelationship));
                command.Parameters.Add(new SqlParameter("@EmergencyContactPhone", patient.EmergencyContactPhone));

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
            using SqlConnection connection = new(_connectionString);
            var result = new List<PatientDBModel>();

            var queryStr = @"SELECT * FROM [EHR-MVC-DB].[dbo].[Patient] WHERE Active = @Active";
            var param = new List<SqlParameter>
            {
                new("@Active", true)
            };

            if (PatientId != null)
            {
                queryStr += " AND PatientId = @PatientId";
                param.Add(new SqlParameter("@PatientId", PatientId));
                Console.WriteLine($"Querying PatientId: {PatientId}"); // Debug
            }
            if (IdNo != null)
            {
                queryStr += " AND IdNo = @IdNo";
                param.Add(new SqlParameter("@IdNo", IdNo));
                Console.WriteLine($"Querying IdNo: {IdNo}"); // Debug
            }
            if (familyName != null)
            {
                queryStr += " AND FamilyName LIKE '%' + @familyName +'%'";
                param.Add(new SqlParameter("@familyName", familyName));
            }
            if (givenName != null)
            {
                queryStr += " AND GivenName LIKE '%' + @givenName +'%'";
                param.Add(new SqlParameter("@givenName", givenName));
            }

            SqlCommand command = new(queryStr, connection);

            foreach (var p in param)
            {
                command.Parameters.Add(p);
            }

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
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
                        Address = reader.GetString(reader.GetOrdinal("Address")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        PostalCode = reader.GetString(reader.GetOrdinal("PostalCode")),
                        Country = reader.GetString(reader.GetOrdinal("Country")),
                        PreferredLanguage = reader.GetString(reader.GetOrdinal("PreferredLanguage")),
                        EmergencyContactName = reader.GetString(reader.GetOrdinal("EmergencyContactName")),
                        EmergencyContactRelationship = reader.GetString(reader.GetOrdinal("EmergencyContactRelationship")),
                        EmergencyContactPhone = reader.GetString(reader.GetOrdinal("EmergencyContactPhone"))
                    };

                    result.Add(patient);
                }
            }
            else
            {
                Console.WriteLine("(404) User Data not found.");
            }
            connection.Close();
            Console.WriteLine($"Query Result Count: {result.Count}"); // Debug
            return result;
        }

        // Update
        public async Task<bool> UpdatePatientAsync(PatientDBModel patient)
        {
            bool result = false;

            try
            {
                using SqlConnection connection = new(_connectionString);
                var updateStr = @"UPDATE [EHR-MVC-DB].[dbo].[Patient]
                              SET 
                                  IdNo = @IdNo, 
                                  Active = @Active, 
                                  FamilyName = @FamilyName, 
                                  GivenName = @GivenName, 
                                  Telecom = @Telecom, 
                                  Gender = @Gender, 
                                  Birthday = @Birthday, 
                                  Address = @Address,
                                  Email = @Email,
                                  PostalCode = @PostalCode,
                                  Country = @Country,
                                  PreferredLanguage = @PreferredLanguage,
                                  EmergencyContactName = @EmergencyContactName,
                                  EmergencyContactRelationship = @EmergencyContactRelationship,
                                  EmergencyContactPhone = @EmergencyContactPhone
                              WHERE PatientId = @PatientId";

                using SqlCommand command = new(updateStr, connection);

                command.Parameters.Add(new SqlParameter("@PatientId", patient.PatientId));
                command.Parameters.Add(new SqlParameter("@IdNo", patient.IdNo));
                command.Parameters.Add(new SqlParameter("@Active", patient.Active));
                command.Parameters.Add(new SqlParameter("@FamilyName", patient.FamilyName));
                command.Parameters.Add(new SqlParameter("@GivenName", patient.GivenName));
                command.Parameters.Add(new SqlParameter("@Telecom", patient.Telecom));
                command.Parameters.Add(new SqlParameter("@Gender", patient.Gender));
                command.Parameters.Add(new SqlParameter("@Birthday", patient.Birthday.ToString("yyyy-MM-dd")));
                command.Parameters.Add(new SqlParameter("@Address", patient.Address));
                command.Parameters.Add(new SqlParameter("@Email", patient.Email));
                command.Parameters.Add(new SqlParameter("@PostalCode", patient.PostalCode));
                command.Parameters.Add(new SqlParameter("@Country", patient.Country));
                command.Parameters.Add(new SqlParameter("@PreferredLanguage", patient.PreferredLanguage));
                command.Parameters.Add(new SqlParameter("@EmergencyContactName", patient.EmergencyContactName));
                command.Parameters.Add(new SqlParameter("@EmergencyContactRelationship", patient.EmergencyContactRelationship));
                command.Parameters.Add(new SqlParameter("@EmergencyContactPhone", patient.EmergencyContactPhone));

                await connection.OpenAsync();

                int rowsAffected = await command.ExecuteNonQueryAsync();
                result = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in UpdatePatientAsync: " + ex.Message);
                throw;
            }

            return result;
        }
    }
}