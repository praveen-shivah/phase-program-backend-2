namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DataPostgresqlLibrary;

    public interface ICheckRefreshToken
    {
        Task<CheckRefreshTokenResponse> Check(DPContext dpContext, CheckRefreshTokenRequest checkRefreshTokenRequest);
    }
}
