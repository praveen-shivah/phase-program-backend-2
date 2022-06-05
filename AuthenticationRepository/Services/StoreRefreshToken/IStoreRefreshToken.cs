namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DataPostgresqlLibrary;

    public interface IStoreRefreshToken
    {
        Task<StoreRefreshTokenResponse> Store(DPContext dpContext, StoreRefreshTokenRequest storeRefreshTokenRequest);
    }
}
