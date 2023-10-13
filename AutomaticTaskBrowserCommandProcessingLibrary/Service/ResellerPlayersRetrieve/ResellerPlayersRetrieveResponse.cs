using PlayersRepositoryTypes;

namespace AutomaticTaskBrowserCommandProcessingLibrary
{

    public enum ResellerPlayersRetrieveResponseType
    {
        start,
        loginCreate,
        loginVerifyLoad,
        loginSubmit,
        logoutCreate,
        logoutVerifyLoad,
        playersReportCreate,
        playersReportVerifyLoad,
        playersReportRetrive,
        apiStore
    }
    public class ResellerPlayersRetrieveResponse
    {
        public bool IsSuccessful { get; set; }

        public ResellerPlayersRetrieveResponseType ResponseType { get; set; }

        public ILoginPage LoginPage { get; set; }

        public IResellerPlayerPage PlayersReportsPage { get; set; }

        public ILogoutPage LogoutPage { get; set; }
        public ResellerPlayersDetail[] Details { get; set; }


    }
}
