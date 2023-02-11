namespace AuthenticationRepository
{
    using DatabaseContext;

    public class AuthenticateUserStart : IAuthenticateUser
    {
        Task<AuthenticateUserResponse> IAuthenticateUser.Authenticate(DataContext dataContext, AuthenticateUserRequest authenticateUserRequest)
        {
            return Task.FromResult(new AuthenticateUserResponse() { IsSuccessful = true });
        }
    }
}
