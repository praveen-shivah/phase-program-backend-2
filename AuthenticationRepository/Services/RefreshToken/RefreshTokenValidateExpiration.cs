namespace AuthenticationRepository
{
    using CommonServices;

    using DatabaseContext;

    public class RefreshTokenValidateExpiration : IRefreshToken
    {
        private readonly IRefreshToken refreshToken;

        private readonly IDateTimeService dateTimeService;

        public RefreshTokenValidateExpiration(IRefreshToken refreshToken, IDateTimeService dateTimeService)
        {
            this.refreshToken = refreshToken;
            this.dateTimeService = dateTimeService;
        }

        async Task<RefreshTokenResponse> IRefreshToken.Refresh(DataContext dataContext, RefreshTokenRequest refreshTokenRequest)
        {
            var response = await this.refreshToken.Refresh(dataContext, refreshTokenRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            if (this.dateTimeService.UtcNow < response.RefreshToken.Expires)
            {
                return response;
            }

            response.IsSuccessful = false;
            response.RefreshTokenResponseType = RefreshTokenResponseType.expired;

            return response;
        }
    }
}
