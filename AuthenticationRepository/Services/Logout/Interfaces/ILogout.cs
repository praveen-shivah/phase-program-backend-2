namespace AuthenticationRepository
{
    using DatabaseContext;

    public interface ILogout
    {
        Task<LogoutResponse> Logout(DataContext dataContext, LogoutRequest logoutRequest);
    }
}
