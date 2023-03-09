namespace AuthenticationRepository
{
    using RestServicesSupportTypes;

    public enum RefreshTokenDtoType
    {
        successful,

        currentTokenNotFound,

        notFound,

        attemptedReuse,

        expired,

        notActive,

        duplicated
    }

    public class ISRefreshTokenResponseDto : BaseResponseDto
    {
        public RefreshTokenDtoType RefreshTokenDtoType { get; set; }

        public string JwtToken { get; set; } = string.Empty;

        public string RefreshToken { get; set; } = string.Empty;
    }
}