using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

namespace testoken.Models
{
    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "Token is required.")]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirm password is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class PasswordResetRepository
    {
        private readonly string connectionString;

        public PasswordResetRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void ResetPassword(ResetPasswordRequest request)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE users SET Password = @Password WHERE Token = @Token";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Token", request.Token);
                    command.Parameters.AddWithValue("@Password", request.Password);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
