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

        Task<List<UpdateUserRequestDto>> GetUsers();

        Task<UpdateUserResponse> UpdateUser(string jwtTokenString, int organizationId, UpdateUserRequestDto updateUserRequestDto);
    }
}
