using System;
using MySql.Data.MySqlClient;

namespace testoken.Models
{
    public class NewToken
    {
        public string Token { get; set; } = string.Empty;

        public DateTime? Start { get; set; } = DateTime.Now;

        public DateTime? End { get; set; }
    }

    public class TokenRepository
    {
        private readonly string connectionString;

        public TokenRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SaveToken(NewToken token)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO tokens (Token, Start, End) VALUES (@Token, @Start, @End)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Token", token.Token);
                    command.Parameters.AddWithValue("@Start", token.Start);
                    command.Parameters.AddWithValue("@End", token.End);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
