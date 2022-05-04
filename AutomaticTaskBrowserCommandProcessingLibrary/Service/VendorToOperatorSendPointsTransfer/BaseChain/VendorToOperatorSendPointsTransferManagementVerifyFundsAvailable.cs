namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorToOperatorSendPointsTransferManagementVerifyFundsAvailable : IVendorToOperatorSendPointsTransferChain
    {
        private readonly IVendorToOperatorSendPointsTransferChain riverSweepsVendorToOperatorSendPointsTransfer;

        public VendorToOperatorSendPointsTransferManagementVerifyFundsAvailable(IVendorToOperatorSendPointsTransferChain riverSweepsVendorToOperatorSendPointsTransfer)
        {
            this.riverSweepsVendorToOperatorSendPointsTransfer = riverSweepsVendorToOperatorSendPointsTransfer;
        }

        VendorToOperatorTransferResponse IVendorToOperatorSendPointsTransferChain.Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.riverSweepsVendorToOperatorSendPointsTransfer.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.VendorToOperatorTransferResponseType = VendorToOperatorTransferResponseType.managementVerifyFundsAvailable;
            response.IsSuccessful = response.ManagementPage.VerifyFundsAvailable(vendorToOperatorSendPointsTransferRequest.Points);

            return response;
        }
    }
}
