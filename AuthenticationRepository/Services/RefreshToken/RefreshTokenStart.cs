namespace AuthenticationRepository
{
    using DatabaseContext;

    public class RefreshTokenStart : IRefreshToken
    {
        Task<RefreshTokenResponse> IRefreshToken.Refresh(DataContext dataContext, RefreshTokenRequest refreshTokenRequest)
        {
            return Task.FromResult(new RefreshTokenResponse()
            {
                IsSuccessful = true,
                RefreshTokenResponseType = RefreshTokenResponseType.successful
            });
        }
    }
}
