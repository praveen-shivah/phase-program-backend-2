namespace AuthenticationRepositoryTypes
{
    public interface IAuthenticationRepository
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest);
    }
}
