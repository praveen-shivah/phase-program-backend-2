namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class RiverSweepsVendorToOperatorSendPointsTransferManagementMakeDeposit : IRiverSweepsVendorToOperatorSendPointsTransfer
    {
        private readonly IRiverSweepsVendorToOperatorSendPointsTransfer riverSweepsVendorToOperatorSendPointsTransfer;

        public RiverSweepsVendorToOperatorSendPointsTransferManagementMakeDeposit(IRiverSweepsVendorToOperatorSendPointsTransfer riverSweepsVendorToOperatorSendPointsTransfer)
        {
            this.riverSweepsVendorToOperatorSendPointsTransfer = riverSweepsVendorToOperatorSendPointsTransfer;
        }

        RiverSweepsVendorToOperatorTransferResponse IRiverSweepsVendorToOperatorSendPointsTransfer.Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.riverSweepsVendorToOperatorSendPointsTransfer.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.RiverSweepsVendorToOperatorTransferResponseType = RiverSweepsVendorToOperatorTransferResponseType.managementMakeDeposit;

            // We'll consider it successful if we get this far so as not to duplicate deposits.
            // Any failure up to this point and we can do a retry.
            response.Management.MakeDeposit(vendorToOperatorSendPointsTransferRequest.Points);

            return response;
        }
    }
}
