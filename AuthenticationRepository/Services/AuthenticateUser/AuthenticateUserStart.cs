namespace AuthenticationRepository
{
    using DatabaseContext;

    using RestServicesSupportTypes;

    public class AuthenticateUserStart : IAuthenticateUser
    {
        Task<AuthenticateUserResponse> IAuthenticateUser.AuthenticateUserAsync(DataContext dataContext, AuthenticateUserRequest authenticateUserRequest)
        {
            return Task.FromResult(new AuthenticateUserResponse()
            {
                IsSuccessful = true,
                Claims = string.Empty,
                ErrorMessage = string.Empty,
                ResponseTypeEnum = ResponseTypeEnum.cannotReach3rdParty
            });
        }
    }
}
