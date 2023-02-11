namespace AuthenticationRepository
{
    using DatabaseContext;

    using Microsoft.EntityFrameworkCore;

    public class LogoutRetrieveUser : ILogout
    {
        private readonly ILogout logout;

        public LogoutRetrieveUser(ILogout logout)
        {
            this.logout = logout;
        }

        async Task<LogoutResponse> ILogout.Logout(DataContext dataContext, LogoutRequest logoutRequest)
        {
            var response = await this.logout.Logout(dataContext, logoutRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            var user = await dataContext.User.Include(x => x.RefreshToken).SingleOrDefaultAsync(x => x.Id == logoutRequest.UserId);
            if (user == null)
            {
                return response;
            }

            dataContext.RefreshToken.RemoveRange(user.RefreshToken);
            user.CurrentRefreshToken = string.Empty;

            return response;
        }
    }
}
