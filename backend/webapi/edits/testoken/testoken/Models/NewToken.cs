namespace testoken.Models
{
    public class NewToken
    {
        public string Token { get; set; } = string.Empty;

        public DateTime Start { get; set; } = DateTime.Now;

        public DateTime End { get; set; }

    }
}
