namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class VendorToOperatorSendPointsTransferLoginSubmit : IVendorToOperatorSendPointsTransferChain
    {
        private readonly IVendorToOperatorSendPointsTransferChain vendorToOperatorSendPointsTransfer;

        public VendorToOperatorSendPointsTransferLoginSubmit(IVendorToOperatorSendPointsTransferChain vendorToOperatorSendPointsTransfer)
        {
            this.vendorToOperatorSendPointsTransfer = vendorToOperatorSendPointsTransfer;
        }

        VendorToOperatorTransferResponse IVendorToOperatorSendPointsTransferChain.Execute(IWebDriver driver, VendorToOperatorSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.vendorToOperatorSendPointsTransfer.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorToOperatorTransferResponseType.loginSubmit;

            response.ManagementPage = response.LoginPage.Submit();
            if (response.ManagementPage == null)
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
