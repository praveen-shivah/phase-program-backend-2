namespace AuthenticationRepository
{
    using DataPostgresqlLibrary;

    public class LogoutStart : ILogout
    {
        Task<LogoutResponse> ILogout.Logout(DPContext dpContext, LogoutRequest logoutRequest)
        {
            return Task.FromResult(new LogoutResponse() { IsSuccessful = true });
        }
    }
}
