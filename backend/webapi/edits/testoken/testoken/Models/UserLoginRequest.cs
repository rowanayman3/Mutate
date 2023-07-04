using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

namespace testoken.Models
{
    public class UserLoginRequest
    {
        // If you want to allow login with username as well
        // [Required(ErrorMessage = "Username is required.")]
        // public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

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

        public void LoginUser(UserLoginRequest request)
        {
            // Perform login logic here
            // You can use the provided Email and Password properties for authentication
        }
    }

