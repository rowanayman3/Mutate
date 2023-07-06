namespace external_end.Models
{
    public class RevockedRefreshToken
    {
        public string Token { get; set; }

        public DateTime expirationDate { get; set;}



    }
}
