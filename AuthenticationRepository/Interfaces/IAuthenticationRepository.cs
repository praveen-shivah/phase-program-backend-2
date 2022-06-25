namespace AuthenticationRepository
{
    using ApiDTO;

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

        Task<LogoutResponse> Logout(LogoutRequest logoutRequest);

        Task<List<UserDto>> GetUsers();

        Task<UpdateUserResponse> UpdateUser(int organizationId, UserDto userDto);
    }
}
