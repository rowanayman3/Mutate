namespace external_end.Models
{
    public class InvalidateToken
    {
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime InvalidateAt { get; set; }

    }
}
