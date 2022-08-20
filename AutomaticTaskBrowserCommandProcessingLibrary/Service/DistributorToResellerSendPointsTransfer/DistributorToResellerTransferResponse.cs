namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public enum DistributorToOperatorTransferResponseType
    {
        start,
        loginCreate,
        loginVerifyLoad,
        loginSubmit,
        logoutCreate,
        logoutVerifyLoad,
        managementCreate,
        managementVerifyLoad,
        managementVerifyFundsAvailable,
        managementMakeLocateAndClickDepositButton,
        managementMakeDeposit
    }

    public class DistributorToResellerTransferResponse
    {
        public bool IsSuccessful { get; set; }

        public DistributorToOperatorTransferResponseType ResponseType { get; set; }

        public ILoginPage LoginPage { get; set; }

        public IManagementPage? ManagementPage { get; set; }

        public ILogoutPage LogoutPage { get; set; }
    }
}
