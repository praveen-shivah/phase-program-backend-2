namespace APISupport
{
    public class ISRefreshTokenRequestDto
    {
        public string RefreshToken { get; set; } = string.Empty;

        public string IPAddress { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;
    }
}