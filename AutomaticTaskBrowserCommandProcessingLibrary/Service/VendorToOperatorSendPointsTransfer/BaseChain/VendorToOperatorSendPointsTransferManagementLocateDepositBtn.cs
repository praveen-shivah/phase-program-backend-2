namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorToOperatorSendPointsTransferManagementLocateDepositBtn : IVendorToOperatorSendPointsTransferChain
    {
        private readonly IVendorToOperatorSendPointsTransferChain riverSweepsVendorToOperatorSendPointsTransfer;

        public VendorToOperatorSendPointsTransferManagementLocateDepositBtn(IVendorToOperatorSendPointsTransferChain riverSweepsVendorToOperatorSendPointsTransfer)
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

            response.VendorToOperatorTransferResponseType = VendorToOperatorTransferResponseType.managementMakeLocateAndClickDepositButton;
            response.IsSuccessful = response.ManagementPage.LocateDepositButtonAndClick(vendorToOperatorSendPointsTransferRequest.DestinationAccountId);

            return response;
        }
    }
}
