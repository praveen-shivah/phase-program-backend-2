namespace AuthenticationRepository
{
    using CommonServices;

    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    public class RefreshTokenRetrieveUser : IRefreshToken
    {
        private readonly IRefreshToken refreshToken;

        private readonly IDateTimeService dateTimeService;

        public RefreshTokenRetrieveUser(IRefreshToken refreshToken, IDateTimeService dateTimeService)
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

            try
            {
                var user = await dataContext.User.Include(x => x.RefreshToken).Include(o => o.Organization).SingleOrDefaultAsync(x => x.CurrentRefreshToken == refreshTokenRequest.RefreshToken);
                if (user == null)
                {
                    response.IsSuccessful = false;
                    response.RefreshTokenResponseType = RefreshTokenResponseType.currentTokenNotFound;
                    return response;
                }

                response.User = user;
                response.UserName = user.UserName;
                response.UserId = user.Id;
            }
            catch
            {
                response.IsSuccessful = false;
                response.RefreshTokenResponseType = RefreshTokenResponseType.duplicated;
            }

            return response;
        }
    }
}
