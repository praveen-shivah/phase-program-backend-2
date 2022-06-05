namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using DataPostgresqlLibrary;

    public class StoreRefreshTokenSave : IStoreRefreshToken
    {
        private readonly IStoreRefreshToken storeRefreshToken;

        public StoreRefreshTokenSave(IStoreRefreshToken storeRefreshToken)
        {
            this.storeRefreshToken = storeRefreshToken;
        }

        async Task<StoreRefreshTokenResponse> IStoreRefreshToken.Store(DPContext dpContext, StoreRefreshTokenRequest storeRefreshTokenRequest)
        {
            var response = await this.storeRefreshToken.Store(dpContext, storeRefreshTokenRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var user = dpContext.User.SingleOrDefault(x => x.Id == storeRefreshTokenRequest.UserId);
            if (user == null)
            {
                response.IsSuccessful = false;
                return response;
            }

            user.RefreshToken = storeRefreshTokenRequest.Token;
            user.RefreshTokenCreated = storeRefreshTokenRequest.Created;
            user.RefreshTokenExpires = storeRefreshTokenRequest.Expires;

            return response;
        }
    }
}
