using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

namespace testoken.Models
{
    public class UserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }

    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateUser(UserDto userDto)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO users (Username, Password) VALUES (@Username, @Password)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", userDto.Username);
                    command.Parameters.AddWithValue("@Password", userDto.Password);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
