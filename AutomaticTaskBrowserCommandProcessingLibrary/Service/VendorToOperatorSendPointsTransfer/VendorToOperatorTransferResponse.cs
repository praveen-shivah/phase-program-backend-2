namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public enum VendorToOperatorTransferResponseType
    {
        start,
        loginCreate,
        loginVerifyLoad,
        loginSubmit,
        managementVerifyFundsAvailable,
        managementMakeLocateAndClickDepositButton,
        managementMakeDeposit
    }

    public class VendorToOperatorTransferResponse
    {
        public bool IsSuccessful { get; set; }

        public VendorToOperatorTransferResponseType VendorToOperatorTransferResponseType { get; set; }

        public IVendorToOperatorTransferLoginPage LoginPage { get; set; }

        public IVendorToOperatorTransferManagementPage ManagementPage { get; set; }
    }
}
