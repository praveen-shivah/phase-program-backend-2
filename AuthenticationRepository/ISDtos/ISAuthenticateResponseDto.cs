namespace APISupport
{
    using RestServicesSupportTypes;

    public class ISAuthenticateResponseDto : BaseResponseDto
    {
        public string AccessToken { get; set; }

        public bool IsAuthenticated { get; set; }

        public ISRefreshTokenResponseDto? RefreshTokenResponseDto { get; set; }

        public string Claims { get; set; }
    }
}