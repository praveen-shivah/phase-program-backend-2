namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DataPostgresqlLibrary;

    public interface IRefreshToken
    {
        Task<RefreshTokenResponse> Refresh(DPContext dpContext, RefreshTokenRequest refreshTokenRequest);
    }
}
