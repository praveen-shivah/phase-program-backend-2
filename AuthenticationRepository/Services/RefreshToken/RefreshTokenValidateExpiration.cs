namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using CommonServices;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    public class RefreshTokenValidateExpiration : IRefreshToken
    {
        private readonly IRefreshToken refreshToken;

        private readonly IDateTimeService dateTimeService;

        public RefreshTokenValidateExpiration(IRefreshToken refreshToken, IDateTimeService dateTimeService)
        {
            this.refreshToken = refreshToken;
            this.dateTimeService = dateTimeService;
        }

        async Task<RefreshTokenResponse> IRefreshToken.Refresh(DPContext dpContext, RefreshTokenRequest refreshTokenRequest)
        {
            var response = await this.refreshToken.Refresh(dpContext, refreshTokenRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            if (!(this.dateTimeService.UtcNow > response.RefreshTokenExpires))
            {
                return response;
            }

            response.IsSuccessful = false;
            response.RefreshTokenResponseType = RefreshTokenResponseType.expired;

            return response;
        }
    }
}
