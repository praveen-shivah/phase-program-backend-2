namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DataPostgresqlLibrary;

    public class StoreRefreshTokenStart : IStoreRefreshToken
    {
        Task<StoreRefreshTokenResponse> IStoreRefreshToken.Store(DPContext dpContext, StoreRefreshTokenRequest storeRefreshTokenRequest)
        {
            return Task.FromResult(new StoreRefreshTokenResponse() { IsSuccessful = true });
        }
    }
}
