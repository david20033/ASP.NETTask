using ASP.NETTask.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ASP.NETTask.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task InsertUsers(List<User> users)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var cmd = new SqlCommand("SET IDENTITY_INSERT Users OFF;", connection))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
                foreach (var user in users)
                {
                    string queryStringUser = "IF EXISTS (select * from Users where Id=@Id) " +
                        "BEGIN " +
                        "UPDATE Users " +
                        "SET Name=@Name, Username=@Username, Password=@Password, Email=@Email, Phone=@Phone, " +
                        "Website=@Website, Note=@Note, IsActive=@IsActive, CreatedAt=@CreatedAt " +
                        "WHERE Id=@Id " +
                        "END " +
                        "ELSE " +
                        "BEGIN " +
                        "INSERT INTO Users " +
                        "(Id, Name, Username, Password, Email, Phone, Website, Note, IsActive, CreatedAt) VALUES " +
                        "(@Id, @Name, @Username, @Password, @Email, @Phone, @Website, @Note, @IsActive, @CreatedAt) " +
                        "END";
                    using (var cmd = new SqlCommand(queryStringUser, connection))
                    {
                        cmd.Parameters.AddWithValue("@Id", user.Id);
                        cmd.Parameters.AddWithValue("@Name", user.Name);
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@Phone", user.Phone);
                        cmd.Parameters.AddWithValue("@Website", user.Website);
                        cmd.Parameters.AddWithValue("@Note", user.Note ?? "");
                        cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                        cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                        await cmd.ExecuteNonQueryAsync();
                    }
                    var userAddress= user.Address;
                    if(userAddress==null) continue;


                    string queryStringAddress = "IF EXISTS (select * from Addresses where UserId=@UserId) " +
                        "BEGIN " +
                        "Update Addresses " +
                        "SET Street=@Street, Suite=@Suite, City=@City, Zipcode=@Zipcode, Lat=@Lat, Lng=@Lng " +
                        "WHERE UserId=@UserId " +
                        "END " +
                        "ELSE " +
                        "BEGIN " +
                        "INSERT INTO Addresses " +
                        "(Street, Suite, City, Zipcode, Lat, Lng, UserId) VALUES " +
                        "(@Street, @Suite, @City, @Zipcode, @Lat, @Lng, @UserId) " +
                        "END";
                    using (var cmd = new SqlCommand(queryStringAddress, connection))
                    {
                        cmd.Parameters.AddWithValue("@Street", userAddress.Street);
                        cmd.Parameters.AddWithValue("@Suite", userAddress.Suite);
                        cmd.Parameters.AddWithValue("@City", userAddress.City);
                        cmd.Parameters.AddWithValue("@Zipcode", userAddress.Zipcode);
                        cmd.Parameters.AddWithValue("@Lat", userAddress.Lat);
                        cmd.Parameters.AddWithValue("@Lng", userAddress.Lng);
                        cmd.Parameters.AddWithValue("@UserId", user.Id);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                using (var cmd = new SqlCommand("SET IDENTITY_INSERT Users ON;", connection))
                {
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
