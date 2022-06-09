namespace AuthenticationRepository
{
    using ApiDTO;

    using DataModelsLibrary;

    public class RefreshTokenResponse
    {
        public bool IsSuccessful { get; set; }

        public RefreshTokenResponseType RefreshTokenResponseType { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public DateTime? RefreshTokenExpires { get; set; }

        public RefreshTokenDto RefreshToken { get; set; }

        public string JwtToken { get; set; }
    }
}
