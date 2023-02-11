namespace AuthenticationRepository
{
    using DatabaseContext;

    public class LogoutStart : ILogout
    {
        Task<LogoutResponse> ILogout.Logout(DataContext dataContext, LogoutRequest logoutRequest)
        {
            return Task.FromResult(new LogoutResponse() { IsSuccessful = true });
        }
    }
}
