namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public enum ResellerBalanceRetrieveResponseType
    {
        start,
        loginCreate,
        loginVerifyLoad,
        loginSubmit,
        logoutCreate,
        logoutVerifyLoad,
        managementCreate,
        managementVerifyLoad,
        managementRetrieveBalance,
        apiStore
    }

    public class ResellerBalanceRetrieveResponse
    {
        public bool IsSuccessful { get; set; }

        public ResellerBalanceRetrieveResponseType ResponseType { get; set; }

        public ILoginPage LoginPage { get; set; }

        public IManagementPage ManagementPage { get; set; }

        public string ResellerBalance { get; set; }

        public ILogoutPage LogoutPage { get; set; }
    }
}
