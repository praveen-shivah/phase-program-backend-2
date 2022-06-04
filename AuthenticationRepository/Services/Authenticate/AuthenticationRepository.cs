namespace AuthenticationRepository
{
    using ApiDTO;

    using AuthenticationRepositoryTypes;

    public class AuthenticationRepository : IAuthenticationRepository
    {
        Task<AuthenticationResponse> IAuthenticationRepository.Authenticate(AuthenticationRequest authenticationRequest)
        {
            return Task.FromResult(
                new AuthenticationResponse()
                    {
                        UserId = 1,
                        UserName = "test",
                        IsAuthenticated = true,
                        IsSuccessful = true
                    });
        }

        Task<CheckRefreshTokenResponse> IAuthenticationRepository.CheckRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        Task IAuthenticationRepository.StoreRefreshToken(int userId, RefreshToken newRefreshToken)
        {
            //user.RefreshToken = newRefreshToken.accessToken;
            //user.TokenCreated = newRefreshToken.Created;
            //user.TokenExpires = newRefreshToken.Expires;

            throw new NotImplementedException();
        }
    }
}
