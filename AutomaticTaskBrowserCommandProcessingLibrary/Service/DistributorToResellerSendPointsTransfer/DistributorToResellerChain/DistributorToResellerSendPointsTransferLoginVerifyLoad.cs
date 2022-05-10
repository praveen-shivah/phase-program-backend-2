namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferLoginVerifyLoad : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain riverSweepsVendorToOperatorSendPointsTransfer;

        public DistributorToResellerSendPointsTransferLoginVerifyLoad(IDistributorToResellerSendPointsTransferChain riverSweepsVendorToOperatorSendPointsTransfer)
        {
            this.riverSweepsVendorToOperatorSendPointsTransfer = riverSweepsVendorToOperatorSendPointsTransfer;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.riverSweepsVendorToOperatorSendPointsTransfer.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorToOperatorTransferResponseType.loginVerifyLoad;
            response.LoginPage.VerifyPageLoaded();

            return response;
        }
    }
}
