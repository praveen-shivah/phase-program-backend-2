namespace AuthenticationRepository
{
    using DataPostgresqlLibrary;

    public interface ILogout
    {
        Task<LogoutResponse> Logout(DPContext dpContext, LogoutRequest logoutRequest);
    }
}
