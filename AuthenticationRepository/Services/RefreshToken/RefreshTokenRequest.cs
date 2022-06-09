namespace AuthenticationRepository
{
    public class RefreshTokenRequest
    {
        public string UserName { get; set; }
        public string RefreshToken { get; set; }
        public string IpAddress { get; set; }
    }
}
