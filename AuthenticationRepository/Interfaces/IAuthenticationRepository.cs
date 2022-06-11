namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    public enum RefreshTokenResponseType
    {
        successful,

        currentTokenNotFound,

        notFound,

        attemptedReuse,

        expired,

        notActive,

        duplicated
    }

    public interface IAuthenticationRepository
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest);

        Task<AuthenticationResponse> GetUserById(int id);

        Task<RefreshTokenResponse> RefreshToken(string refreshToken, int userId, string ipAddress);
    }
}
