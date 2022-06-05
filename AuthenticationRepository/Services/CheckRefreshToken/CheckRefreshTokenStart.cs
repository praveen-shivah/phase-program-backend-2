namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DataPostgresqlLibrary;

    public class CheckRefreshTokenStart : ICheckRefreshToken
    {
        Task<CheckRefreshTokenResponse> ICheckRefreshToken.Check(DPContext dpContext, CheckRefreshTokenRequest checkRefreshTokenRequest)
        {
            return Task.FromResult(new CheckRefreshTokenResponse()
            {
                IsSuccessful = true,
                CheckRefreshTokenResponseType = CheckRefreshTokenResponseType.successful
            });
        }
    }
}
