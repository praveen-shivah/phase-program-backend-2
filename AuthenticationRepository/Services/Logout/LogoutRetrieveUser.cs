namespace AuthenticationRepository
{
    using DataPostgresqlLibrary;

    using Microsoft.EntityFrameworkCore;

    public class LogoutRetrieveUser : ILogout
    {
        private readonly ILogout logout;

        public LogoutRetrieveUser(ILogout logout)
        {
            this.logout = logout;
        }

        async Task<LogoutResponse> ILogout.Logout(DPContext dpContext, LogoutRequest logoutRequest)
        {
            var response = await this.logout.Logout(dpContext, logoutRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var user = await dpContext.User.Include(x => x.RefreshTokens).SingleOrDefaultAsync(x => x.Id == logoutRequest.UserId);
            if (user == null)
            {
                return response;
            }

            dpContext.RefreshToken.RemoveRange(user.RefreshTokens);
            user.CurrentRefreshToken = string.Empty;

            return response;
        }
    }
}
