namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    public enum RiverSweepsVendorToOperatorTransferResponseType
    {
        start,
        loginCreate,
        loginVerifyLoad,
        loginSubmit,
        managementVerifyFundsAvailable,
        managementMakeLocateAndClickDepositButton,
        managementMakeDeposit
    }

    public class RiverSweepsVendorToOperatorTransferResponse
    {
        public bool IsSuccessful { get; set; }

        public RiverSweepsVendorToOperatorTransferResponseType RiverSweepsVendorToOperatorTransferResponseType { get; set; }

        public RiverSweepsVendorToOperatorTransferLogin Login { get; set; }

        public RiverSweepsVendorToOperatorTransferShopsManagement Management { get; set; }
    }
}
