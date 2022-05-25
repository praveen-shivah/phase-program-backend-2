namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    public class AuthenticationRepository : IAuthenticationRepository
    {
        Task<AuthenticationResponse> IAuthenticationRepository.Authenticate(AuthenticationRequest authenticationRequest)
        {
            return Task.FromResult(
                new AuthenticationResponse()
                    {
                        IsAuthenticated = true,
                        IsSuccessful = true
                    });
        }
    }
}
