namespace AuthenticationRepository
{
    using DatabaseContext;

    public interface IAuthenticateUser
    {
        Task<AuthenticateUserResponse> AuthenticateUserAsync(DataContext dataContext, AuthenticateUserRequest authenticateUserRequest);
    }
}
