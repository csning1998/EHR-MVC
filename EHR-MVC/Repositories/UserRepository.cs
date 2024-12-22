using EHR_MVC.Models.Users;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EHR_MVC.Repositories
{
    public class UserRepository(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        // Insert
        public async Task<long> InsertUserAsync(UserDBModel user)
        {
            long insertId = 0;

            using (SqlConnection connection = new(_connectionString))
            {
                var insertStr = @"
                    INSERT INTO [EHR-MVC-DB].[dbo].[Users]
                        (FamilyName, GivenName, UserEmail, PasswordHashed, Role, CreatedAt)
                    VALUES
                        (@FamilyName, @GivenName, @UserEmail, @PasswordHashed, @Role, @CreatedAt);
                    SELECT @InsertId = SCOPE_IDENTITY();
                ";

                using SqlCommand command = new(insertStr, connection);
                SqlParameter outputValue = new("@InsertId", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };

                command.Parameters.Add(outputValue);
                command.Parameters.Add(new SqlParameter("@UserEmail", user.UserEmail));
                command.Parameters.Add(new SqlParameter("@PasswordHashed", user.PasswordHashed));
                command.Parameters.Add(new SqlParameter("@FamilyName", user.FamilyName));
                command.Parameters.Add(new SqlParameter("@GivenName", user.GivenName));
                command.Parameters.Add(new SqlParameter("@Role",
                   string.IsNullOrEmpty(user.Role) ? "Basic" : user.Role)); 
                command.Parameters.Add(new SqlParameter("@CreatedAt", user.CreatedAt));

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                connection.Close();

                if (outputValue.Value != DBNull.Value)
                {
                    insertId = Convert.ToInt64(outputValue.Value);
                }
            }

            return insertId;
        }

        // Select: For user to log in.
        public async Task<UserDBModel?> GetUserByEmailAsync(string email)
        {
            using SqlConnection connection = new(_connectionString);
            var queryStr = @"
                SELECT * FROM [EHR-MVC-DB].[dbo].[Users] 
                WHERE UserEmail = @UserEmail;
            ";
            using SqlCommand command = new(queryStr, connection);
            command.Parameters.Add(new SqlParameter("@UserEmail", email));

            await connection.OpenAsync();

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            if (reader.Read())
            {
                return new UserDBModel
                {
                    UserId = reader.GetInt64(reader.GetOrdinal("UserId")),
                    UserEmail = reader.GetString(reader.GetOrdinal("UserEmail")),
                    PasswordHashed = reader.GetString(reader.GetOrdinal("PasswordHashed")),
                };
            }

            return null;
        }
    }
}
