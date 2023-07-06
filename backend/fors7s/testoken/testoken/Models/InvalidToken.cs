namespace external_end.Models
{
    public class InvalidToken
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime InvalidateAt { get; set; } 

    }
}
