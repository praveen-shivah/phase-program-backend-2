namespace AuthenticationRepository
{
    using AuthenticationRepositoryTypes;

    using CommonServices;

    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    public class CheckRefreshTokenRetrieve : ICheckRefreshToken
    {
        private readonly ICheckRefreshToken checkRefreshToken;

        private readonly IDateTimeService dateTimeService;

        public CheckRefreshTokenRetrieve(ICheckRefreshToken checkRefreshToken, IDateTimeService dateTimeService)
        {
            this.checkRefreshToken = checkRefreshToken;
            this.dateTimeService = dateTimeService;
        }

        async Task<CheckRefreshTokenResponse> ICheckRefreshToken.Check(DPContext dpContext, CheckRefreshTokenRequest checkRefreshTokenRequest)
        {
            var response = await this.checkRefreshToken.Check(dpContext, checkRefreshTokenRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var user = await dpContext.User.SingleOrDefaultAsync(x => x.RefreshToken == checkRefreshTokenRequest.RefreshToken);
            if (user == null)
            {
                response.IsSuccessful = false;
                response.CheckRefreshTokenResponseType = CheckRefreshTokenResponseType.notFound;
                return response;
            }

            if (this.dateTimeService.UtcNow > user.RefreshTokenExpires )
            {
                response.IsSuccessful = false;
                response.CheckRefreshTokenResponseType = CheckRefreshTokenResponseType.expired;
            }

            return response;
        }
    }
}
