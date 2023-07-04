using System;
using System.ComponentModel.DataAnnotations;
using MySql.Data.MySqlClient;

namespace testoken.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];

        public string? VerificationToken { get; set; }

        public DateTime? VerifyAt { get; set; }

        public string? PasswordResetToken { get; set; }

        public DateTime? PasswordResetExpire { get; set; }

        public string NewToken { get; set; } = string.Empty;

        public DateTime TokenStart { get; set; }

        public DateTime TokenEnd { get; set; }
    }

    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void CreateUser(User user)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO users (Email, Username, PasswordHash, PasswordSalt, VerificationToken, VerifyAt, PasswordResetToken, PasswordResetExpire, NewToken, TokenStart, TokenEnd) " +
                               "VALUES (@Email, @Username, @PasswordHash, @PasswordSalt, @VerificationToken, @VerifyAt, @PasswordResetToken, @PasswordResetExpire, @NewToken, @TokenStart, @TokenEnd)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    command.Parameters.AddWithValue("@PasswordSalt", user.PasswordSalt);
                    command.Parameters.AddWithValue("@VerificationToken", user.VerificationToken);
                    command.Parameters.AddWithValue("@VerifyAt", user.VerifyAt);
                    command.Parameters.AddWithValue("@PasswordResetToken", user.PasswordResetToken);
                    command.Parameters.AddWithValue("@PasswordResetExpire", user.PasswordResetExpire);
                    command.Parameters.AddWithValue("@NewToken", user.NewToken);
                    command.Parameters.AddWithValue("@TokenStart", user.TokenStart);
                    command.Parameters.AddWithValue("@TokenEnd", user.TokenEnd);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
