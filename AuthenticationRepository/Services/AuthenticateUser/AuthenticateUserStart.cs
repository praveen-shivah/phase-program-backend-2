namespace AuthenticationRepository
{
    using DataPostgresqlLibrary;

    public class AuthenticateUserStart : IAuthenticateUser
    {
        Task<AuthenticateUserResponse> IAuthenticateUser.Authenticate(DPContext dpContext, AuthenticateUserRequest authenticateUserRequest)
        {
            return Task.FromResult(new AuthenticateUserResponse() { IsSuccessful = true });
        }
    }
}
