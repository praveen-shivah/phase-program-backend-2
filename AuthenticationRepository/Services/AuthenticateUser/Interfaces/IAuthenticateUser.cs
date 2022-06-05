namespace AuthenticationRepository
{
    using DataPostgresqlLibrary;

    public interface IAuthenticateUser
    {
        Task<AuthenticateUserResponse> Authenticate(DPContext dpContext, AuthenticateUserRequest authenticateUserRequest);
    }
}
