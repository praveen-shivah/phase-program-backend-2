namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public enum VendorToOperatorTransferResponseType
    {
        start,
        loginCreate,
        loginVerifyLoad,
        loginSubmit,
        managementCreate,
        managementVerifyLoad,
        managementVerifyFundsAvailable,
        managementMakeLocateAndClickDepositButton,
        managementMakeDeposit
    }

    public class DistributorToResellerTransferResponse
    {
        public bool IsSuccessful { get; set; }

        public VendorToOperatorTransferResponseType ResponseType { get; set; }

        public ILoginPage LoginPage { get; set; }

        public IManagementPage ManagementPage { get; set; }
    }
}
