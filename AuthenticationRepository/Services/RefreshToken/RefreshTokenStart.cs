namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DataPostgresqlLibrary;

    public class RefreshTokenStart : IRefreshToken
    {
        Task<RefreshTokenResponse> IRefreshToken.Refresh(DPContext dpContext, RefreshTokenRequest refreshTokenRequest)
        {
            return Task.FromResult(new RefreshTokenResponse()
            {
                IsSuccessful = true,
                RefreshTokenResponseType = RefreshTokenResponseType.successful
            });
        }
    }
}
