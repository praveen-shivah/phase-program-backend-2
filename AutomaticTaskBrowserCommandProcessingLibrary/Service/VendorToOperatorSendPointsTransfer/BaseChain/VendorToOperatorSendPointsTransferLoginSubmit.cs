namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorToOperatorSendPointsTransferLoginSubmit : IVendorToOperatorSendPointsTransferChain
    {
        private readonly IVendorToOperatorSendPointsTransferChain riverSweepsVendorToOperatorSendPointsTransfer;

        public VendorToOperatorSendPointsTransferLoginSubmit(IVendorToOperatorSendPointsTransferChain riverSweepsVendorToOperatorSendPointsTransfer)
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

            response.VendorToOperatorTransferResponseType = VendorToOperatorTransferResponseType.loginSubmit;

            response.ManagementPage = response.LoginPage.Submit();
            if (response.ManagementPage == null)
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
