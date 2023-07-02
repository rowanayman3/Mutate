using System;
using MySql.Data.MySqlClient;
namespace testoken.Models
{
    public class EmailDto
    {
        public string To { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;  

    }
      public class EmailRepository
    {
        private readonly string connectionString;

        public EmailRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SaveEmail(EmailDto email)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO emails (ToAddress, Subject, Body) VALUES (@To, @Subject, @Body)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@To", email.To);
                    command.Parameters.AddWithValue("@Subject", email.Subject);
                    command.Parameters.AddWithValue("@Body", email.Body);
                    command.ExecuteNonQuery();
                }
            }
        }
      }
 }
