namespace AutomaticTaskBrowserCommandProcessingLibrary
{
    using OpenQA.Selenium;

    public class DistributorToResellerSendPointsTransferLoginSubmit : IDistributorToResellerSendPointsTransferChain
    {
        private readonly IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransfer;

        public DistributorToResellerSendPointsTransferLoginSubmit(IDistributorToResellerSendPointsTransferChain vendorToOperatorSendPointsTransfer)
        {
            this.vendorToOperatorSendPointsTransfer = vendorToOperatorSendPointsTransfer;
        }

        DistributorToResellerTransferResponse IDistributorToResellerSendPointsTransferChain.Execute(IWebDriver driver, DistributorToResellerSendPointsTransferRequest vendorToOperatorSendPointsTransferRequest)
        {
            var response = this.vendorToOperatorSendPointsTransfer.Execute(driver, vendorToOperatorSendPointsTransferRequest);
            if (!response.IsSuccessful)
            {
                return response;
            }

            response.ResponseType = VendorToOperatorTransferResponseType.loginSubmit;
            response.IsSuccessful = response.LoginPage.Submit();
            if (response.ManagementPage == null)
            {
                response.IsSuccessful = false;
            }

            return response;
        }
    }
}
