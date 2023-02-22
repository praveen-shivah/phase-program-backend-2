namespace AuthenticationRepository
{
    using DatabaseContext;

    public class AuthenticateUserStart : IAuthenticateUser
    {
        Task<AuthenticateUserResponse> IAuthenticateUser.AuthenticateUserAsync(DataContext dataContext, AuthenticateUserRequest authenticateUserRequest)
        {
            return Task.FromResult(new AuthenticateUserResponse() { IsSuccessful = true });
        }
    }
}
