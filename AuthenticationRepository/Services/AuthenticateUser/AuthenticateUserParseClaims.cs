namespace AuthenticationRepository;

using DatabaseContext;

public class AuthenticateUserParseClaims : IAuthenticateUser
{
    private IAuthenticateUser authenticateUser;

    public AuthenticateUserParseClaims(IAuthenticateUser authenticateUser)
    {
        this.authenticateUser = authenticateUser;
    }

    async Task<AuthenticateUserResponse> IAuthenticateUser.AuthenticateUserAsync(DataContext context, AuthenticateUserRequest request)
    {
        var response = await this.authenticateUser.AuthenticateUserAsync(context, request);
        if (!response.IsSuccessful)
        {
            return response;
        }

        return response;
    }
}