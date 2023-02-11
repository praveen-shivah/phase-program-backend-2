namespace AuthenticationRepository
{
    using DatabaseContext;

    public interface IAuthenticateUser
    {
        Task<AuthenticateUserResponse> Authenticate(DataContext dataContext, AuthenticateUserRequest authenticateUserRequest);
    }
}
